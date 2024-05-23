#pragma warning disable 649
#pragma warning disable 169

namespace Anc.Certification.Api.Automation.Tests.Models
{
    using System;
    public class ExamResponce
    {

        public class Rootobject
        {
            public int PersonId { get; set; }
            public string CertificationTypeEnum { get; set; }
            public int ApplicationId { get; set; }
            public string ExamStatusEnum { get; set; }
            public string ExamResolutionStatusEnum { get; set; }
            public string ExamTypeEnum { get; set; }
            public string ExamLanguageEnum { get; set; }
            public int ExamRescoringStatus { get; set; }
            public object GroupExamId { get; set; }
            public object GroupExam { get; set; }
            public string ExamNotTakenReasonEnum { get; set; }
            public bool Passed { get; set; }
            public Specialaccommodations SpecialAccommodations { get; set; }
            public object DateStartEffectiveEligibility { get; set; }
            public object DateEndEffectiveEligibility { get; set; }
            public object DateOfExam { get; set; }
            public object DatePaid { get; set; }
            public object DateScheduled { get; set; }
            public object Scheduled { get; set; }
            public Identification Identification { get; set; }
            public Eligibilityqueue EligibilityQueue { get; set; }
            public object SelectedFormId { get; set; }
            public bool AdminIgnoreExam { get; set; }
            public string ExamVendorEnum { get; set; }
            public int ReferenceOrderId { get; set; }
            public object ExternalOrderId { get; set; }
            public object ExternalOrderLineItemId { get; set; }
            public string ExamVendorCountry { get; set; }
            public DateTime SystemCreateDate { get; set; }
            public bool IsPilotExam { get; set; }
            public string ExamLanguageAsText { get; set; }
            public bool IsActive { get; set; }
            public bool IsOpen { get; set; }
            public bool IsFailed { get; set; }
            public bool IsScheduled { get; set; }
            public bool IsEligible { get; set; }
            public string ExamVendorAsText { get; set; }
            public string DateEndEffectiveEligibilityAsText { get; set; }
            public Links Links { get; set; }
        }

        public class Specialaccommodations
        {
            public bool Requested { get; set; }
            public object Approved { get; set; }
            public object PreviousRequested { get; set; }
            public object LimitDescription { get; set; }
            public object RequireTestAccommodation { get; set; }
            public object TestAccommodationDescription { get; set; }
            public object Conditions { get; set; }
            public object ConditionsList { get; set; }
            public object[] ConditionsEnumList { get; set; }
        }

        public class Identification
        {
            public Name Name { get; set; }
            public Address Address { get; set; }
        }

        public class Name
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public object MiddleName { get; set; }
            public string LastName { get; set; }
            public string FullName { get; set; }
        }

        public class Address
        {
            public int Id { get; set; }
            public object Attention { get; set; }
            public object OrganizationName { get; set; }
            public string Address1 { get; set; }
            public object Address2 { get; set; }
            public object Address3 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string PostalCode { get; set; }
            public string CountryCode { get; set; }
            public string AddressTypeEnum { get; set; }
        }

        public class Eligibilityqueue
        {
            public object SentCandidateId { get; set; }
            public object ConfirmationNumber { get; set; }
            public object ExamId { get; set; }
            public object ExamFormId { get; set; }
            public object ExamFormCode { get; set; }
            public int ExamSequence { get; set; }
            public string ExamQueueStatusEnum { get; set; }
            public object ReturnSiteId { get; set; }
            public int NumberAttempts { get; set; }
            public bool Suspended { get; set; }
            public string SuspendedReason { get; set; }
            public object ExamResultReceived { get; set; }
            public object SuspendedDate { get; set; }
        }

        public class Links
        {
            public Self Self { get; set; }
        }

        public class Self
        {
            public string Href { get; set; }
            public string[] Allowed { get; set; }
        }
    }

}
