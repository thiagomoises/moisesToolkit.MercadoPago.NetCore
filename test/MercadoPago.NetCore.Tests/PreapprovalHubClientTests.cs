﻿using MercadoPago.NetCore.Model.Resources;
using MercadoPago.NetCore.Model.Resources.Dataclassures.Auth;
using MercadoPago.NetCore.Model.Resources.Dataclassures.Preapproval;
using Moises.Toolkit.MercadoPago.NetCore.HubClients;
using Moises.Toolkit.MercadoPago.NetCore.HubClients.Abstracts;
using Moises.Toolkit.MercadoPago.NetCore.Tests.Helpers;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading.Tasks.Extensions;

namespace Moises.Toolkit.MercadoPago.NetCore.Tests
{
    [TestFixture]
    public class PreapprovalHubClientTests
    {
        private PreapprovalHubClient _PreapprovalHubClient { get; set; }
        private string CustomerId { get; set; }

        [SetUp]
        public void Setup()
        {
            Mock<ITokenHubClient> tokenHubClient = new Mock<ITokenHubClient>();

            Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();

            _httpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>()))
                .Returns(TicketHelperTest.GetHttpClient());


            var customerHub = new CustomerHubClientTests();
            customerHub.Setup();
            if (customerHub.CustomerHubClient.SearchAsync(null).TryExecute(out var customers))
                CustomerId = customers.Results.Select(x => x.Id).FirstOrDefault();

            tokenHubClient.Setup(x => x.GetTicketAsync())
                .Returns(Task.FromResult(new Ticket() { AccessToken = "TEST-8600607042428103-060407-f91bbb3d5d0029bc342657a83aa08ee5-397002962" }));
            _PreapprovalHubClient = new PreapprovalHubClient(_httpClientFactory.Object, TicketHelperTest.GetMPOptions(), tokenHubClient.Object);
        }

        [TearDown]
        public void Cleanup()
        {
            _PreapprovalHubClient = null;
        }
        /*
        [Test]
        public async Task SaveAsync_Test()
        {
            var Preapproval = new Preapproval()
            {
                CustomerId = CustomerId,
                ExpirationMonth = 9,
                ExpirationYear = 2022,
                FirstSixDigits = "123456",
                LastFourDigits = "7890",
                SecurityCode = new SecurityCode() { PreapprovalLocation = "back", Length = 3 },
                PreapprovalHolder = new PreapprovalHolder { Name = "Thiago Moises" },
                DateCreated = DateTime.Now
            };
            var result = await _PreapprovalHubClient.SaveAsync(Preapproval);

            bool firstCondition = (!string.IsNullOrEmpty(result?.Id) && Preapproval.FirstSixDigits.Equals(result?.FirstSixDigits) && _PreapprovalHubClient.IsValid());
            bool secondCondition = _PreapprovalHubClient.Notifications.Any(x => x.Message.Equals("the Preapproval already exist"));

            Assert.IsTrue(firstCondition || secondCondition);
        }
        */
        [Test]
        public async Task FindAsync_Test_Null()
        {
            var result = await _PreapprovalHubClient.FindAsync(null);
            Assert.IsTrue(_PreapprovalHubClient.IsInvalid());
        }

        [Test]
        public async Task FindAsync_Test_Empty()
        {
            var result = await _PreapprovalHubClient.FindAsync( "");
            Assert.IsTrue(_PreapprovalHubClient.IsInvalid());
        }

        [Test]
        public async Task SaveAsync_Test_Null()
        {
            var result = await _PreapprovalHubClient.SaveAsync(null);
            Assert.IsTrue(_PreapprovalHubClient.IsInvalid());
        }

        [Test]
        public async Task UpdateAsync_Test()
        {
            var result = await _PreapprovalHubClient.UpdateAsync(null);
            Assert.IsTrue(_PreapprovalHubClient.IsInvalid());
        }
        
    }
}
