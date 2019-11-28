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
                new PermanentEmployee("Nicole Perkins",20000,500, new DateTimeOffset(2019,3,5,0,0,0,TimeSpan.Zero)),
                new PermanentEmployee("Nicole Perkins",20000,500, new DateTimeOffset(2019,3,5,0,0,0,TimeSpan.Zero)),
                new PermanentEmployee("Margo Bailey",11111,444, new DateTimeOffset(2019,4,5,0,0,0,TimeSpan.Zero)),
                new PermanentEmployee("Ashton Botwright",22222,333, new DateTimeOffset(2019,5,5,0,0,0,TimeSpan.Zero)),
                new PermanentEmployee("Suzanne Hunnisett",33333,222, new DateTimeOffset(2019,6,5,0,0,0,TimeSpan.Zero)),
                new PermanentEmployee("Devin Disney",66666,888, new DateTimeOffset(2019,7,5,0,0,0,TimeSpan.Zero)),
                new PermanentEmployee("Before March31 ",66666,888, new DateTimeOffset(2019,3,30,0,0,0,TimeSpan.Zero)),
                new PermanentEmployee("Ater March31",66666,888, new DateTimeOffset(2019,4,1,0,0,0,TimeSpan.Zero)),
            };
        }

        public static List<TempEmployee> GetTempEmployees()
        {
            return new List<TempEmployee>
            {
                new TempEmployee("Seth Cox",34, new DateTimeOffset(2019,8,5,0,0,0,TimeSpan.Zero)),
                new TempEmployee("Eimhir Lukeson",10, new DateTimeOffset(2019,9,5,0,0,0,TimeSpan.Zero)),
                new TempEmployee("Patrick Maynard",34, new DateTimeOffset(2019,10,5,0,0,0,TimeSpan.Zero)),
                new TempEmployee("Duncan Queen",50, new DateTimeOffset(2019,11,5,0,0,0,TimeSpan.Zero)),
                new TempEmployee("Tilly Duff",100, new DateTimeOffset(2019,12,5,0,0,0,TimeSpan.Zero)),
            };
        }
    }
}