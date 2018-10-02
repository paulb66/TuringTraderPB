﻿//==============================================================================
// Project:     Trading Simulator
// Name:        DataUpdaterWeb
// Description: Web data updater
// History:     2018ix27, FUB, created
//------------------------------------------------------------------------------
// Copyright:   (c) 2017-2018, Bertram Solutions LLC
//              http://www.bertram.solutions
// License:     this code is licensed under GPL-3.0-or-later
//==============================================================================

#region libraries
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace FUB_TradingSim
{
    public class DataUpdaterWeb : DataUpdater
    {
        #region internal data
        private static readonly DateTime _epochOrigin = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        #endregion
        #region internal helpers
        private long DateTimeToEpoch(DateTime t)
        {
            return (long)Math.Floor((t.ToUniversalTime() - _epochOrigin).TotalSeconds);
        }
        #endregion

        #region public DataUpdateWeb(Dictionary<DataSourceValue, string> info) : base(info)
        public DataUpdaterWeb(Dictionary<DataSourceValue, string> info) : base(info)
        {
        }
        #endregion

        #region override List<Bar> void UpdateData(DateTime startTime, DateTime endTime)
        override public IEnumerable<Bar> UpdateData(DateTime startTime, DateTime endTime)
        {
            return null;

            // examples:
            //   stooq.com:
            //     updateWeb=https://stooq.com/q/d/l/?s=^spx&d1={0:yyyy}{0:MM}{0:dd}&d2={5:yyyy}{5:MM}{5:dd}&i=d
            //              =https://stooq.com/q/d/l/?s=^spx&d1=20050101&d2=20180927&i=d

            string url = string.Format(
                Info[DataSourceValue.updateWeb],
                //--- startTime
                startTime,                  // 0: as DateTime
                DateTimeToEpoch(startTime), // 1: as epoch
                0,
                0,
                0,
                //--- endTime
                endTime,                    // 5: as DateTime
                DateTimeToEpoch(endTime),   // 6: as epoch
                0,
                0,
                0);

            using (var client = new WebClient())
            {
                //return client.DownloadString(url);
            }
        }
        #endregion

        #region public override string Name
        public override string Name
        {
            get
            {
                return "Web";
            }
        }
        #endregion
    }
}

//==============================================================================
// end of file