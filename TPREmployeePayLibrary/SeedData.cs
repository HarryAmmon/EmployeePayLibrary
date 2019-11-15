using System;
using System.Collections.Generic;
using System.Text;
using TPREmployeePayLibrary.Entites;

namespace TPREmployeePayLibrary
{
    public class SeedData
    {
        public static List<PermanentEmployee> GetPermanentEmployees()
        {
            return new List<PermanentEmployee>
            {
                new PermanentEmployee("harry0",20000,500, new DateTimeOffset(2019,3,5,0,0,0,TimeSpan.Zero)),
                new PermanentEmployee("harry1",11111,444, new DateTimeOffset(2019,4,5,0,0,0,TimeSpan.Zero)),
                new PermanentEmployee("james",22222,333, new DateTimeOffset(2019,5,5,0,0,0,TimeSpan.Zero)),
                new PermanentEmployee("matt",33333,222, new DateTimeOffset(2019,6,5,0,0,0,TimeSpan.Zero)),
                new PermanentEmployee("richard",66666,888, new DateTimeOffset(2019,7,5,0,0,0,TimeSpan.Zero)),
            };
        }

        public static List<TempEmployee> GetTempEmployees()
        {
            return new List<TempEmployee>
            {
                new TempEmployee("richard0",34, new DateTimeOffset(2019,8,5,0,0,0,TimeSpan.Zero)),
                new TempEmployee("richard1",10, new DateTimeOffset(2019,9,5,0,0,0,TimeSpan.Zero)),
                new TempEmployee("harry",34, new DateTimeOffset(2019,10,5,0,0,0,TimeSpan.Zero)),
                new TempEmployee("samantha",50, new DateTimeOffset(2019,11,5,0,0,0,TimeSpan.Zero)),
                new TempEmployee("isabel",100, new DateTimeOffset(2019,12,5,0,0,0,TimeSpan.Zero)),
            };
        }
    }
}