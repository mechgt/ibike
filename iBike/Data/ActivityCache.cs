using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Visuals;

namespace iBike.Data
{
    static class ActivityCache
    {
        private static List<iBikeActivity> activities = new List<iBikeActivity>();

        /// <summary>
        /// Gets the iBikeActivity associated with an activity
        /// </summary>
        /// <param name="refId">ReferenceId of requested activity</param>
        /// <returns>Matching iBikeActivity, newly created iBikeActivity, or null if it doesn't exist</returns>
        internal static iBikeActivity GetIBikeActivity(string refId)
        {
            foreach (iBikeActivity activity in activities)
            {
                if (activity.ReferenceId == refId)
                {
                    // Return existing iBikeActivity
                    return activity;
                }
            }

            foreach (IActivity activity in PluginMain.GetApplication().Logbook.Activities)
            {
                if (activity.ReferenceId == refId)
                {
                    // Return New (empty) iBikeActivity
                    iBikeActivity newActivity = new iBikeActivity(activity);
                    activities.Add(newActivity);

                    return newActivity;
                }
            }

            // No matching records exist
            return null;
        }

        /// <summary>
        /// Add iBike activity to cache.  Option to overwrite activity with new activity if it's already existing in the cache.
        /// </summary>
        /// <param name="activity"></param>
        /// <param name="overwrite"></param>
        internal static void AddIBikeActivity(iBikeActivity activity, bool overwrite)
        {
            // Do nothing if null activity encountered
            if (activity == null)
            {
                return;
            }

            // Remove if already existing?
            foreach (iBikeActivity cached in activities)
            {
                if (cached.ReferenceId == activity.ReferenceId)
                {
                    if (overwrite)
                    {
                        activities.Remove(cached);
                        break;
                    }
                    else
                    {
                        return;
                    }
                }
            }

            // Cache new activity
            activities.Add(activity);
        }

        /// <summary>
        /// Remove iBike Activity from cache.
        /// </summary>
        /// <param name="activity"></param>
        internal static void PurgeActivity(IActivity activity)
        {
            foreach (iBikeActivity iBike in activities)
            {
                // TODO: Why use the starttime for Purgeing cache when we should be using the refId...?  I'm sure there's a reason?
                if (iBike.StartTime == activity.StartTime)
                {
                    // Remove activity from cache
                    activities.Remove(iBike);
                    return;
                }
            }
        }
    }
}
