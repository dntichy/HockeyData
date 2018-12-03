using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HockeyPlayerDatabase.Interfaces;

namespace HockeyPlayerDatabase.MainApp
{
    public class WrapperClubPlayer 
    {
        public WrapperClubPlayer()
        {
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; }
        public string TitleBefore { get; set; }
        public int YearOfBirth { get; set; }
        public int KrpId { get; set; }
        public AgeCategory? AgeCategory { get; set; }
        public int? ClubId { get; set; }
        public string ClubName { get; set; }
        public string Url { get; set; }
        
  
    }
}
