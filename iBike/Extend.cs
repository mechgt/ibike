using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using iBike.Data;
using iBike.Importer;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Visuals;

namespace iBike
{
    class Extend : IExtendDataImporters, IExtendActivityDetailPages, IExtendDailyActivityViewActions, IExtendActivityReportsViewActions
    {
        #region IExtendDataImporters Members

        public void AfterImport(IList added, IList updated)
        {
        }

        public void BeforeImport(IList items)
        {
        }

        public IList<IFileImporter> FileImporters
        {
            get
            {
                return new IFileImporter[] { new iBike.Importer.FileImporter() };
            }
        }

        #endregion

        #region IExtendActivityDetailPages Members

        public IList<IDetailPage> GetDetailPages(IDailyActivityView view, ExtendViewDetailPages.Location location)
        {
            return new List<IDetailPage> { new iBike.DetailPage.iBikePane(view) };
        }

        #endregion

        #region IExtendDailyActivityViewActions Members

        public IList<IAction> GetActions(IDailyActivityView view, ExtendViewActions.Location location)
        {
            if (location == ExtendViewActions.Location.EditMenu)
            {
                IList<IAction> list = new List<IAction>();
                list.Add(new Actions.MergeIBikeFile(view, true)); // Manual import (select your own file)
                list.Add(new Actions.MergeIBikeFile(view, false)); // Auto file selection
                return list;
            }

            return null;
        }

        #endregion

        #region IExtendActivityReportsViewActions Members

        public IList<IAction> GetActions(IActivityReportsView view, ExtendViewActions.Location location)
        {
            IList<IAction> list = new List<IAction>();
            list.Add(new Actions.MergeIBikeFile(view, true)); // Manual import (select your own file)
            list.Add(new Actions.MergeIBikeFile(view, false)); // Auto file selection
            return list;
        }

        #endregion
    }
}
