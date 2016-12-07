using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace XFApp2.Views
{
    public class TabbedMasterDetailPage : MasterDetailPage, IViewContainer<Page>, IPageContainer<Page>,
        IItemsView<Page>
    {
        private readonly ObservableCollection<Page> _children;

        private Page _currentPage;
        public EventHandler CurrentPageChanged;

        public TabbedMasterDetailPage()
        {
            _children = new ObservableCollection<Page>();
            _children.CollectionChanged += ChildrenOnCollectionChanged;
            var masterListview = new ListView
            {
                ItemsSource = _children,
                ItemTemplate = new DataTemplate(typeof(TextCell))
                {
                    Bindings =
                    {
                        new KeyValuePair<BindableProperty, BindingBase>(TextCell.TextProperty, new Binding("Title"))
                    }
                }
            };
            masterListview.ItemSelected += MasterListviewOnItemSelected;
            base.Master = new ContentPage
            {
                Title = "Title",
                Content = masterListview
            };
            var l = new ListView();
            base.Detail = new NavigationPage();
        }

        public new Page Detail => base.Detail;
        public new Page Master => base.Master;

        public Page CreateDefault(object item)
        {
            var page = new Page();
            if (item != null)
                page.Title = item.ToString();

            return page;
        }

        void IItemsView<Page>.SetupContent(Page content, int index)
        {
        }

        void IItemsView<Page>.UnhookContent(Page content)
        {
        }

        public Page CurrentPage
        {
            get { return _currentPage; }
            set
            {
                if (_currentPage == value) return;
                _currentPage = value;
                OnPropertyChanged();
                CurrentPageChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public IList<Page> Children => _children;



        private void MasterListviewOnItemSelected(object sender,
            SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            base.Detail = new NavigationPage(selectedItemChangedEventArgs.SelectedItem as Page);
            IsPresented = false;
        }


        private void ChildrenOnCollectionChanged(object sender,
            NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            switch (notifyCollectionChangedEventArgs.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if ((base.Detail as NavigationPage)?.CurrentPage == null)
                        CurrentPage = notifyCollectionChangedEventArgs.NewItems[0] as Page;
                    break;
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == nameof(CurrentPage))
                base.Detail = new NavigationPage(CurrentPage);
            base.OnPropertyChanged(propertyName);
        }
    }
}