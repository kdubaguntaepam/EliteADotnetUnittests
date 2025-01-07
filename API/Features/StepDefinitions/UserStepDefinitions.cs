using AutomationFramwork.API.Core.Models;
using AutomationFramwork.API.Core.Services;
using AutomationFramwork.API.Framework.ApiClients;
using AutomationFramwork.API.Framework.Utilities;
using NUnit.Framework;
using BoDi;
using TechTalk.SpecFlow;

namespace AutomationFramwork.API.Features.StepDefinitions
{
    [Binding]
    public class UserStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly UserService _userService;
        private User _user;

        public UserStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            // Initialize the UserService with a real or mocked API client
            var apiClient = new UserApiClient(ConfigReader.GetBaseUrl());
            _userService = new UserService(apiClient);
        }

        [Given(@"the user ID is (.*)")]
        public void GivenTheUserIdIs(int id)
        {
            _scenarioContext["UserId"] = id; // Store the value in the injected ScenarioContext
        }

        [When(@"I request the user details")]
        public async Task WhenIRequestTheUserDetails()
        {
            var userId = (int)_scenarioContext["UserId"];
            _user = await _userService.GetUserAsync(userId);
        }

        [Then(@"the response should contain the user's name and email")]
        public void ThenTheResponseShouldContainTheUsersNameAndEmail()
        {
            Assert.That(_user, Is.Not.Null, "User object should not be null");
            Assert.That(_user.Name, Is.Not.Null.Or.Empty, "User's name should not be null or empty");
            Assert.That(_user.Email, Is.Not.Null.Or.Empty, "User's email should not be null or empty");
        }
    }
}
