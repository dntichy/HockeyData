using System.ComponentModel;

namespace HockeyPlayerDatabase.ImportDataApp

{
    partial class Program
    {
        enum EClubs
        {
            [Description("HC Košice s.r.o.")]
            HC_KOSICE,
            [Description("HC SLOVAN Bratislava, a.s.")] 
            HC_SLOVAN_BA,
            [Description("MsHK Žilina, a.s.")]
            MSHK_ZILINA,
            [Description("MHC Martin, a.s.")]
            MHC_MARTIN,
            [Description("HK DUKLA Trenčín, a.s.")]
            HK_DUKLA_TRENCIN
        }
    }
}