﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Meow.Controllers;
using Meow.Models.Account;
using Meow.Code.DAL;
using Meow.Code.Model;
using Rhino.Mocks;
using System.Data.Entity;

namespace MeowTest
{
    [TestClass]
    public class TestAccountController
    {

        private AccountController accountController;

        private IMeowContext meowContext;

        private Dictionary<string, Cat> cats = new Dictionary<string, Cat>();

        [TestInitialize]
        public void setup()
        {
            cats.Clear();
            meowContext = MockRepository.GenerateMock<IMeowContext>();
            meowContext.Expect(f => f.AddCat(Arg<Cat>.Is.Anything)).WhenCalled(mi =>
            {
                var cat = (Cat)mi.Arguments[0];
                cats.Add(cat.Username, cat);
            }).Repeat.Once();
            meowContext.Expect(f => f.AddCat(Arg<Cat>.Is.Anything)).Throw(new AccountCreationException("Duplicate account"));

            accountController = new AccountController(meowContext);
        }

        [TestMethod]
        public void TestRegisterCat()
        {
            var registerCatModel = new RegisterCatModel() { Username = "testCat1", Email = "testMail@mail.com", Password = "test123" };
            accountController.RegisterCat(registerCatModel);

            meowContext.AssertWasCalled(ctx => ctx.AddCat(Arg<Cat>.Matches(c => c.Username.Equals("testCat1"))));
            meowContext.AssertWasCalled(ctx => ctx.Save());

            Assert.AreEqual(1, cats.Count);
            var theCat = cats["testCat1"];
            Assert.IsNotNull(theCat);
            Assert.AreEqual("testMail@mail.com", theCat.Email);
        }

        [TestMethod]
        [ExpectedException(typeof(AccountCreationException))]
        public void TestRegisterCatUniqueUsername()
        {
            var registerCatModel = new RegisterCatModel() { Username = "testCat1", Email = "testMail@mail.com", Password = "test123" };
            var registerCatModel2 = new RegisterCatModel() { Username = "testCat1", Email = "testMail@mail.com", Password = "test1234" };
            accountController.RegisterCat(registerCatModel);

            meowContext.AssertWasCalled(ctx => ctx.AddCat(Arg<Cat>.Matches(c => c.Username.Equals("testCat1"))));
            meowContext.AssertWasCalled(ctx => ctx.Save());

            accountController.RegisterCat(registerCatModel2);
        }
    }
}
