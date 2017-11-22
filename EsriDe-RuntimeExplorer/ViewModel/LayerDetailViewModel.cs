﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using CsvHelper;
using CsvHelper.TypeConversion;
using Esri.ArcGISRuntime;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Mapping;
using EsriDe.RuntimeExplorer.CsvConverter;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Win32;

namespace EsriDe.RuntimeExplorer.ViewModel
{
    public class LayerDetailViewModel : ViewModelBase
    {
        public Map Map { get; set; }

        public LayerDetailViewModel()
        {
            try
            {
                var instance = SimpleIoc.Default.GetInstance<MainDataViewModel>();
                Map = instance.SelectedMapView.Map;
	
                ExportToCsvCommand = new RelayCommand(() =>
                {
                    var dlg = new SaveFileDialog {Filter = "Comma Separated Values|*.csv"};
                    if (dlg.ShowDialog() == true)
                    {
                        using (var sw = new StreamWriter(dlg.FileName))
                        using (var csv = new CsvWriter(sw))
                        {
                            csv.Configuration.TypeConverterCache.AddConverter<FeatureTable>(new FeatureTableTypeConverter());
                            csv.WriteRecords(FeatureLayers);
                        }
                    }
                });

                //der Wechsel eines Tabs interessiert mich
                instance.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(MainDataViewModel.SelectedMapView))
                    {
                        Map = instance.SelectedMapView.Map;
                        //der Wechsel einer Map innerhalb eines Tabs interessiert mich auch
                        instance.SelectedMapView.PropertyChanged += (sender1, args1) =>
                        {
                            if (args1.PropertyName == nameof(MapViewModel.Map))
                            {
                                Map = instance.SelectedMapView.Map;
                            }
                        };
                    }
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
	    public ICommand ExportToCsvCommand { get; private set; }
        }


        public IEnumerable<FeatureLayer> FeatureLayers => Map?.OperationalLayers?.OfType<FeatureLayer>();

        public IEnumerable<RasterLayer> RasterLayers => Map?.OperationalLayers?.OfType<RasterLayer>();
        
        
    }
}