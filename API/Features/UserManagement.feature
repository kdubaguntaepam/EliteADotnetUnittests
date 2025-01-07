Feature: User Management

Scenario: Get a user by ID
	Given the user ID is 1
	When I request the user details
	Then the response should contain the user's name and email
