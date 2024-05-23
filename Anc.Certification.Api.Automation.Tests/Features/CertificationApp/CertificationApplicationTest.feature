Feature: Certification API Automation

This feature covers the Certification API Automation

@Qa @Can
Scenario: Verify experience for an application
	Given User able to access the certification application
    When  Add experience
    Then  Verify experience is added

@Qa @Can
Scenario: Get and verify exam details
	Given User able to get the exam details
	Then  Verify exam details
