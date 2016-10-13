using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoDirectXPlayer.bean;
using SaleSupport.database;
using basic;
using VideoDirectXPlayer.srt;

namespace VideoDirectXPlayer.dao
{
    public class SrtInfoDao
    {
        public static List<SrtInfo> getSrtInfoOfEpidose(int eid){
            List<SrtInfo> srtInfos = new List<SrtInfo>();
            Dictionary<int, Dictionary<string, string>> dict = DBExecMgr.ExecuteSelectSql("SELECT s.*,t.fromtime,t.totime FROM srtinfo s,timeline t where s.id=t.id and episode_id=" + eid);
            SrtInfo srt ;
            foreach (Dictionary<string, string> val in dict.Values)
            {
                srt = new SrtInfo();
                foreach (KeyValuePair<string, string> kv in val)
                {
                    if(basic.BasicStringUtil.equalsIgnoreCase("chs",kv.Key)){
                        srt.setChs(kv.Value);
                    }
                    if (basic.BasicStringUtil.equalsIgnoreCase("eng", kv.Key))
                    {
                        srt.setEng(kv.Value);
                    }
                    if (basic.BasicStringUtil.equalsIgnoreCase("id", kv.Key))
                    {
                        srt.setDbId(BasicNumberUtil.getInt32(kv.Value));
                    }
                    if (basic.BasicStringUtil.equalsIgnoreCase("fromtime", kv.Key))
                    {
                        srt.setFromTime(TimeHelper.parseTimeInfo(kv.Value));
                    }
                    if (basic.BasicStringUtil.equalsIgnoreCase("totime", kv.Key))
                    {
                        srt.setToTime(TimeHelper.parseTimeInfo(kv.Value));
                    }
                }
                srtInfos.Add(srt);
            }
            return srtInfos;
        }
    }
}
