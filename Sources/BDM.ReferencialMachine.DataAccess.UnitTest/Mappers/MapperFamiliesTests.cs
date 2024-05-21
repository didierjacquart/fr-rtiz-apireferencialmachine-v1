using System;
using System.Collections.Generic;
using System.Linq;
using BDM.ReferencialMachine.Core.Model;
using BDM.ReferencialMachine.DataAccess.Mappers;
using BDM.ReferencialMachine.DataAccess.Models;
using Xunit;

namespace BDM.ReferencialMachine.DataAccess.UnitTest.Mappers
{
    public class MapperFamiliesTests
    {
        [Fact]
        public void Should_Map_Many_T_FAMILY_Nominal_Call()
        {
            var tFamilies = new List<T_FAMILY>
            {
                new T_FAMILY
                {
                    FAMILY_ID = Guid.NewGuid(),
                    SUB_CODE = "SubCod_A",
                    SUB_NAME = "SubName_A",
                    CODE = "Code_A",
                    NAME = "Name_A"
                },
                new T_FAMILY {FAMILY_ID = Guid.NewGuid(), CODE = "Code_A", NAME = "Name_A", SUB_CODE = "ROOT" },
                new T_FAMILY {FAMILY_ID = Guid.NewGuid(), CODE = "Code_B", NAME = "Name_B", SUB_CODE = "ROOT" },
                new T_FAMILY
                {
                    FAMILY_ID = Guid.NewGuid(),
                    SUB_CODE = "SubCod_B1",
                    SUB_NAME = "SubName_B1",
                    CODE = "Code_B",
                    NAME = "Name_B"
                },
                new T_FAMILY
                {
                    FAMILY_ID = Guid.NewGuid(),
                    SUB_CODE = "SubCod_B2",
                    SUB_NAME = "SubName_B2",
                    CODE = "Code_B",
                    NAME = "Name_B"
                }
            };

            var mapperFamilies = new MapperFamilies();
            var families = mapperFamilies.Map(tFamilies);

            Assert.NotNull(families);
            Assert.Equal(2, families.Count);

            var expectedFamilies = tFamilies.Where(x => x.SUB_CODE == "ROOT").GroupBy(x => x.CODE).Select(y => y.First());
            var expectedSubFamilies = tFamilies.Where(x => x.SUB_CODE != "ROOT");

            foreach (var family in families)
            {
                Assert.Equal(1, expectedFamilies.Count(x => x.CODE == family.Code));
                var expectedCountSubFamilies =
                    expectedSubFamilies.Where(x => x.CODE == family.Code).Count();
                var subFamilies = family.SubFamilies.Where(x => x.Code != "ROOT");
                Assert.Equal(expectedCountSubFamilies, subFamilies.Count());

                foreach (var subFamily in subFamilies)
                {
                    Assert.Equal(1, expectedSubFamilies.Count(x => x.SUB_CODE == subFamily.Code));
                }
            }
        }

        [Fact]
        public void Should_Map_One_T_FAMILY_Nominal_Call()
        {
            var expectedDbFamily = new T_FAMILY
            {
                FAMILY_ID = new Guid(),
                SUB_CODE = "SubCod_A",
                SUB_NAME = "SubName_A",
                CODE = "Code_A",
                NAME = "Name_A"
            };
            var expectedDbFamilies = new List<T_FAMILY>
            {
                expectedDbFamily
            };

            var mapperFamilies = new MapperFamilies();
            var families = mapperFamilies.Map(expectedDbFamilies);

            Assert.NotNull(families);
            Assert.Single(families);

            var family = families.FirstOrDefault();
            Assert.Equal(expectedDbFamily.CODE, family.Code);
            Assert.Equal(expectedDbFamily.NAME, family.Name);

            var subFamilies = family.SubFamilies;
            Assert.NotNull(subFamilies);
            Assert.Single(subFamilies);

            var subFamily = subFamilies.FirstOrDefault();
            Assert.Equal(expectedDbFamily.SUB_CODE, subFamily.Code);
            Assert.Equal(expectedDbFamily.SUB_NAME, subFamily.Name);

        }
        [Fact]
        public void Should_Map_ManyBusinessFamilies_Nominal_Call()
        {
            var businessFamilies = new List<Family>
            {
                new Family
                {
                    Code = "Code_A",
                    Name = "Name_A",
                    SubFamilies = new List<SubFamily>
                    {
                        new SubFamily
                        {
                            Code = "Sub_Code_A",
                            Name = "Sub_Name_A"
                        },
                        new SubFamily
                        {
                            Code = "ROOT"
                        }
                    }
                },
                new Family
                {
                    Code = "Code_B",
                    Name = "Name_B",
                    SubFamilies = new List<SubFamily>
                    {
                        new SubFamily
                        {
                            Code = "Sub_Code_B1",
                            Name = "Sub_Name_B1"
                        },
                        new SubFamily
                        {
                            Code = "Sub_Code_B2",
                            Name = "Sub_Name_B2"
                        },
                        new SubFamily
                        {
                            Code = "ROOT"
                        }
                    }
                },
                new Family
                {
                    Code = "Code_C",
                    Name = "Name_C",
                    SubFamilies = new List<SubFamily>
                    {
                        new SubFamily
                        {
                            Code = "ROOT"
                        }
                    }
                }

            };

            var expectedDbFamilies = new List<T_FAMILY>
            {
                new T_FAMILY {CODE = "Code_A", NAME = "Name_A", SUB_CODE = "Sub_Code_A", SUB_NAME = "Sub_Name_A"},
                new T_FAMILY {CODE = "Code_A", NAME = "Name_A", SUB_CODE = "ROOT", SUB_NAME = null},
                new T_FAMILY {CODE = "Code_B", NAME = "Name_B", SUB_CODE = "Sub_Code_B1", SUB_NAME = "Sub_Name_B1"},
                new T_FAMILY {CODE = "Code_B", NAME = "Name_B", SUB_CODE = "Sub_Code_B2", SUB_NAME = "Sub_Name_B2"},
                new T_FAMILY {CODE = "Code_B", NAME = "Name_B", SUB_CODE = "ROOT", SUB_NAME = null},
                new T_FAMILY {CODE = "Code_C", NAME = "Name_C", SUB_CODE = "ROOT", SUB_NAME = null}
            };

            var mapperFamilies = new MapperFamilies();
            var families = mapperFamilies.Map(businessFamilies);

            Assert.NotNull(families);
            Assert.Equal(expectedDbFamilies.Count, families.Count);

            foreach (var expected in expectedDbFamilies)
            {
                Assert.Equal(expectedDbFamilies.Count(x => x.CODE == expected.CODE), families.Count(x => x.CODE == expected.CODE));
                Assert.Equal(expectedDbFamilies.Count(x => x.NAME == expected.NAME), families.Count(x => x.NAME == expected.NAME));
                Assert.Equal(expectedDbFamilies.Count(x => x.SUB_CODE == expected.SUB_CODE), families.Count(x => x.SUB_CODE == expected.SUB_CODE));
                Assert.Equal(expectedDbFamilies.Count(x => x.SUB_NAME == expected.SUB_NAME), families.Count(x => x.SUB_NAME == expected.SUB_NAME));
            }
        }

        [Fact]
        public void Should_Map_One_MapSpecificationFamily_Nominal_Call()
        {
            var businessFamilies = new List<Family>
            {
                new Family
                {
                    Code = "Code_A",
                    Name = "Name_A",
                    SubFamilies = new List<SubFamily>
                    {
                        new SubFamily {Name = "SubName_A", Code = "SubCode_A"}
                    }
                }
            };

            var expectedDbFamily = new T_FAMILY
            {
                FAMILY_ID = new Guid(),
                SUB_CODE = "SubCode_A",
                SUB_NAME = "SubName_A",
                CODE = "Code_A",
                NAME = "Name_A"
            };

            var mapperFamilies = new MapperFamilies();
            var families = mapperFamilies.Map(businessFamilies);

            Assert.NotNull(families);
            Assert.Single(families);

            var family = families.FirstOrDefault();
            
            Assert.Equal(expectedDbFamily.CODE, family.CODE);
            Assert.Equal(expectedDbFamily.NAME, family.NAME);
            Assert.Equal(expectedDbFamily.SUB_CODE, family.SUB_CODE);
            Assert.Equal(expectedDbFamily.SUB_NAME, family.SUB_NAME);

        }


        [Fact]
        public void Should_Map_MapSpecificationFamily_When_Input_Is_Null()
        {
            
            var mapperFamilies = new MapperFamilies();
            var dbFamilies = mapperFamilies.Map((ICollection<Family>)null);

            Assert.Null(dbFamilies);
        }

        [Fact]
        public void Should_Map_Many_T_FAMILY_When_Input_Is_Null()
        {
            
            var mapperFamilies = new MapperFamilies();
            var families = mapperFamilies.Map((ICollection<T_FAMILY>)null);
            Assert.Null(families);
        }
    }
}
