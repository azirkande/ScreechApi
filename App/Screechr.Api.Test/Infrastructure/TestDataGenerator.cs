using Screechr.Core.Data.Entities;
using System;
using System.Collections.Generic;

namespace Screechr.Api.Test.Infrastructure
{
    public static class TestDataGenerator
    {
        public static Tuple<List<User>, List<Screech>> SeedInitialData()
        {
            var users = new List<User>()
                {
                    new User {CreatedOn =  DateTime.UtcNow, ModifiedOn =DateTime.UtcNow, FirstName="Ams", LastName ="Dhange", Id =123,ProfileImageUrl="http://s3Bucket.com/mypicturez", Secret ="demo123",UserName="amritaz"},
                                        new User {CreatedOn =  DateTime.UtcNow, ModifiedOn =DateTime.UtcNow, FirstName="Prashant", LastName ="Zirkande", Id =444,ProfileImageUrl="http://s3Bucket.com/mypictured", Secret ="pass123",UserName="demouser"},
                    new User {CreatedOn =  DateTime.UtcNow, ModifiedOn =DateTime.UtcNow, FirstName="Prashant", LastName ="Zirkande", Id =456,ProfileImageUrl="http://s3Bucket.com/mypictured", Secret ="xdssyhrjh9eww",UserName="prashantd"}
                };

            var screeches = new List<Screech>()
            {
                new Screech { Id = 777, Contents ="Something funny", CreatedBy = 456 , DateCreated =  DateTime.UtcNow.AddDays (1), DateModified =  DateTime.UtcNow},
                new Screech { Id = 888, Contents ="Something not funny", CreatedBy = 456 , DateCreated =  DateTime.UtcNow.AddDays (2), DateModified =  DateTime.UtcNow},
                 new Screech { Id = 999, Contents ="Something not funny", CreatedBy = 123 , DateCreated =  DateTime.UtcNow.AddDays (3), DateModified =  DateTime.UtcNow}
            };
            return new Tuple<List<User>, List<Screech>>(users, screeches);

        }
    }
}
