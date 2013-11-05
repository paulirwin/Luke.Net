using Caliburn.Micro;
using Luke.Net.Lib;
using Luke.Net.Lib.Index;
using System;
using System.Windows;

namespace Luke.Net.ViewModels
{
    public class ShellViewModel : Screen
    {
        private readonly IWindowManager _windowManager;
        private readonly SimpleContainer _container;
        private readonly IIndexService _indexService;

        private string _indexPath;

        public ShellViewModel(IWindowManager windowManager, 
            SimpleContainer container, 
            IIndexService indexService,
            OverviewViewModel overview)
        {
            _windowManager = windowManager;
            _container = container;
            _indexService = indexService;

            this.DisplayName = "Luke.Net - Lucene Index Toolbox, v" + typeof(ShellViewModel).Assembly.GetName().Version.ToString();
        }

        private void ShowOpenIndexModal()
        {
            var browser = _container.GetInstance<IFolderBrowser>();
            var messageBox = _container.GetInstance<IMessageBox>();

            var vm = new OpenIndexViewModel(this, browser, messageBox);

            vm.Cancel += CancelLoadIndex;
            vm.Okay += LoadIndex;

            _windowManager.ShowDialog(vm);
        }

        void LoadIndex(object sender, EventArgs e)
        {
            var vm = sender as OpenIndexViewModel;

            var options = new IndexLoadOptions();
            options.ForceUnlock = vm.ForceUnlock;
            options.ReadOnly = vm.OpenReadOnly;

            bool success = _indexService.TryLoad(vm.PathToIndex, options);

            if (!success)
            {
                vm.LoadFailed();
                return;
            }

            IndexPath = vm.PathToIndex;

            vm.TryClose();
        }

        void CancelLoadIndex(object sender, EventArgs e)
        {
            if (!_indexService.IsLoaded)
                base.TryClose();
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            ShowOpenIndexModal();
        }

        public string IndexPath
        {
            get { return _indexPath; }
            set
            {
                _indexPath = value;
                NotifyOfPropertyChange(() => IndexPath);
            }
        }
    }
}