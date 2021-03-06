/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:EsriDe.RuntimeExplorer"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace EsriDe.RuntimeExplorer.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            Initialize();
        }

        public static void Initialize()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<MainDataViewModel>();
            SimpleIoc.Default.Register<LayerDetailViewModel>();
            SimpleIoc.Default.Register<AboutContentViewModel>();
        }

        public MainViewModel Main
        {
            get { return ServiceLocator.Current.GetInstance<MainViewModel>(); }
        }

        public MainDataViewModel MainData
        {
            get { return ServiceLocator.Current.GetInstance<MainDataViewModel>(); }
        }

        public LayerDetailViewModel LayerDetail
        {
            get { return ServiceLocator.Current.GetInstance<LayerDetailViewModel>(); }
        }
        public AboutContentViewModel AboutInformation
        {
            get { return ServiceLocator.Current.GetInstance<AboutContentViewModel>(); }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}