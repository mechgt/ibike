using iBike.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Visuals.Util;

namespace iBike.DetailPage
{
    class iBikePane : IDetailPage
    {
        #region Fields

        //private IActivity activity;
        private static iBikeDetail control;
        private static bool visible = false;
        private static bool maximized;
        private static IDailyActivityView view;

        #endregion

        #region Constructors

        internal iBikePane(IDailyActivityView view)
        {
            iBikePane.view = view;
        }

        void SelectionProvider_SelectedItemsChanged(object sender, EventArgs e)
        {
            IActivity activity = CollectionUtils.GetSingleItemOfType<IActivity>(view.SelectionProvider.SelectedItems);
            Instance.Activity = activity;
        }

        #endregion

        #region Properties

        internal static bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        internal static iBikeDetail Instance
        {
            get
            {
                if (control == null)
                {
                    control = new iBikeDetail();
                }

                return control;
            }
        }

        #endregion

        #region IDialogPage Members

        public Control CreatePageControl()
        {
            return Instance;
        }

        public bool HidePage()
        {
            visible = false;
            view.SelectionProvider.SelectedItemsChanged -= new EventHandler(SelectionProvider_SelectedItemsChanged);
            return true;
        }

        public string PageName
        {
            get
            {
                return Strings.Label_iBike;
            }
        }

        public void ShowPage(string bookmark)
        {
            // NOTE: All eval restrictions removed
            //if (Mechgt.Licensing.Licensing.IsActivated)
            //{
            //    control.ScreenLock(false);
            //}

            visible = true;
            view.SelectionProvider.SelectedItemsChanged += new EventHandler(SelectionProvider_SelectedItemsChanged);
            SelectionProvider_SelectedItemsChanged(null, null);
        }

        public IPageStatus Status
        {
            get
            {
                return null;
            }
        }

        public void ThemeChanged(ITheme visualTheme)
        {
            Instance.ThemeChanged(visualTheme);
        }

        public string Title
        {
            get
            {
                return Strings.Label_iBike;
            }
        }

        public void UICultureChanged(CultureInfo culture)
        {
            CreatePageControl();
            control.UICultureChanged(culture);
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region IDetailPage Members

        public Guid Id
        {
            get { return GUIDs.iBikePane; }
        }

        public bool MenuEnabled
        {
            get { return true; }
        }

        public IList<string> MenuPath
        {
            get { return new List<string> { CommonResources.Text.LabelPower }; }
        }

        public bool MenuVisible
        {
            get { return true; }
        }

        public bool PageMaximized
        {
            get
            {
                return maximized;
            }
            set
            {
                maximized = value;

                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("PageMaximized"));
                }
            }
        }

        #endregion
    }
}
