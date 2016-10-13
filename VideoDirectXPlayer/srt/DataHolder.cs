using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoDirectXPlayer.bean;

namespace VideoDirectXPlayer.srt
{
    public class DataHolder
    {
        private static string fileKey;
        private static int curIndex = 0;
        private static Dictionary<string, List<SrtInfo>> dict = new Dictionary<string, List<SrtInfo>>();

        public static void switchFile(string file){
            fileKey = file;
        }

        public static void product(String file,List<SrtInfo> list){
            if (!dict.ContainsKey(file))
                dict.Add(file, list);
            else
            {
                dict.Remove(file);
                dict.Add(file, list);
            }
        }
        public static SrtInfo getCurrent(){
            return dict[fileKey][curIndex];
        }
        public static SrtInfo getNext()
        {
            return dict[fileKey][++curIndex];
        }
        public static SrtInfo getPre()
        {
            return dict[fileKey][--curIndex];
        }

        public static List<SrtInfo> getAllSrtInfos()
        {
            if (fileKey == null || !dict.ContainsKey(fileKey))
            {
                return null;
            }
            return  dict[fileKey];
        }
    }
}
