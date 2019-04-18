using ImsGlobal.Caliper.Util;
using NUnit.Framework;

namespace ImsGlobal.Caliper.Tests {
	using ImsGlobal.Caliper.Entities;
	using ImsGlobal.Caliper.Entities.Agent;
	using ImsGlobal.Caliper.Entities.Annotation;
	using ImsGlobal.Caliper.Entities.Assessment;
	using ImsGlobal.Caliper.Entities.Assignable;
	using ImsGlobal.Caliper.Entities.Collection;
	using ImsGlobal.Caliper.Entities.Forum;
	using ImsGlobal.Caliper.Entities.Lis;
	using ImsGlobal.Caliper.Entities.Media;
	using ImsGlobal.Caliper.Entities.Outcome;
	using ImsGlobal.Caliper.Entities.Reading;
	using ImsGlobal.Caliper.Entities.Response;
	using ImsGlobal.Caliper.Entities.Session;
	using ImsGlobal.Caliper.Events;
	using ImsGlobal.Caliper.Events.Annotation;
	using ImsGlobal.Caliper.Events.Assessment;
	using ImsGlobal.Caliper.Events.Assignable;
	using ImsGlobal.Caliper.Events.Forum;
	using ImsGlobal.Caliper.Events.Media;
	using ImsGlobal.Caliper.Events.Outcome;
	using ImsGlobal.Caliper.Events.Reading;
    using ImsGlobal.Caliper.Events.Search;
    using ImsGlobal.Caliper.Events.Session;
	using ImsGlobal.Caliper.Events.Tool;
	using ImsGlobal.Caliper.Tests.SimpleHelpers;
	using ImsGlobal.Caliper.Protocol;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;
	using NodaTime;
	using static JsonSerializeUtils;
	using System.Collections;
	using ImsGlobal.Caliper.Entities.ToolLaunch;
    using ImsGlobal.Caliper.Entities.AggregateMeasure;
    using ImsGlobal.Caliper.Entities.Search;
    using System.Linq;
    using ImsGlobal.Caliper.Entities.Feedback;

    [TestFixture]
	public class Caliper11Tests {
        private static readonly ICaliperContext defaultContextV1p1 = new CaliperContext("http://purl.imsglobal.org/ctx/caliper/v1p1");
        private static readonly ICaliperContext searchProfileExtensionV1p1 = new CaliperContext("http://purl.imsglobal.org/ctx/caliper/v1p1/SearchProfile-extension");
        private static readonly ICaliperContext toolLaunchProfileExtensionV1p1 = new CaliperContext("http://purl.imsglobal.org/ctx/caliper/v1p1/ToolLaunchProfile-extension");
        private static readonly ICaliperContext toolUseProfileExtensionV1p1 = new CaliperContext("http://purl.imsglobal.org/ctx/caliper/v1p1/ToolUseProfile-extension");

        [OneTimeSetUp]
		public void Init() {
			FixtureCoverageChecker.Initialize();
		}

		[OneTimeTearDown]
		public void Dispose() {
			bool covered = FixtureCoverageChecker.Compare();
		}

        [SetUp]
        public void TestSetUp()
        {
            JsonAssertions.CaliperVersion = "v1p1";
        }

		[Test]
		public void EntityAgent_MatchesReferenceJson() {
			var entity = new Agent("https://example.edu/agents/99999", defaultContextV1p1) {
				DateCreated = CaliperTestEntities.Instant20160801060000,
				DateModified = CaliperTestEntities.Instant20160902113000
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityAgent");
		} 

		[Test]
		public void EntityAnnotation_MatchesReferenceJson() {
			var entity = new Annotation("https://example.com/users/554433/texts/imscaliperimplguide/annotations/1", defaultContextV1p1) {
				Annotator = CaliperTestEntities.Person554433(defaultContextV1p1),
				Annotated = new Page("https://example.com/#/texts/imscaliperimplguide/cfi/6/10!/4/2/2/2@0:0", defaultContextV1p1) { HideCaliperContext = true },
				DateCreated = CaliperTestEntities.Instant20160801060000,
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityAnnotation");
		}

		[Test]
		public void EntityAssessment_MatchesReferenceJson() {

			string itemUrlBase = "https://example.edu/terms/201601/courses/7/sections/1/assess/1/items/";

			var entity = new Assessment(
				"https://example.edu/terms/201601/courses/7/sections/1/assess/1", defaultContextV1p1) {
				Name = "Quiz One",
				Items = new[] {
					new AssessmentItem(itemUrlBase + "1", defaultContextV1p1) { HideCaliperContext = true },
					new AssessmentItem(itemUrlBase + "2", defaultContextV1p1) { HideCaliperContext = true },
					new AssessmentItem(itemUrlBase + "3", defaultContextV1p1) { HideCaliperContext = true }
                },
				DateCreated = Instant.FromUtc(2016, 8, 1, 6, 0, 0),
				DateModified = Instant.FromUtc(2016, 9, 2, 11, 30, 0),
				DatePublished = Instant.FromUtc(2016, 8, 15, 9, 30, 0),
				DateToActivate = Instant.FromUtc(2016, 8, 16, 5, 0, 0),
				DateToShow = Instant.FromUtc(2016, 8, 16, 5, 0, 0),
				DateToStartOn = Instant.FromUtc(2016, 8, 16, 5, 0, 0),
				DateToSubmit = Instant.FromUtc(2016, 9, 28, 11, 59, 59),
				MaxAttempts = 2,
				MaxScore = 15.0, //TODO is set as int in spec
				MaxSubmits = 2,
				Version = "1.0"
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityAssessment");
		}

		[Test]
		public void EntityAssessmentItemExtended_MatchesReferenceJson() {

			var entity = new AssessmentItem(
				"https://example.edu/terms/201601/courses/7/sections/1/assess/1/items/3", defaultContextV1p1) {

				IsPartOf = new Assessment("https://example.edu/terms/201601/courses/7/sections/1/assess/1", defaultContextV1p1) { HideCaliperContext = true },
				DateCreated = Instant.FromUtc(2016, 8, 1, 6, 0, 0),
				DatePublished = Instant.FromUtc(2016, 8, 15, 9, 30, 0),
				IsTimeDependent = false,
				MaxScore = 1.0,
				MaxSubmits = 2,
				Extensions = new ExtensionObject()
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityAssessmentItemExtended");
		}

		class ExtensionObject {
			[JsonProperty("questionType", Order = 91)]
			public string QuestionType = "Dichotomous";

			[JsonProperty("questionText", Order = 92)]
			public string QuestionText = "Is a Caliper SoftwareApplication a subtype of Caliper Agent?";

			[JsonProperty("correctResponse", Order = 93)]
			public string CorrectResponse = "yes";
		}

		[Test]
		public void EntityAssignableDigitalResource_MatchesReferenceJson() {

			var entity = new AssignableDigitalResource(
				"https://example.edu/terms/201601/courses/7/sections/1/assign/2", defaultContextV1p1) {

				Name = "Week 9 Reflection",
				Description = "3-5 page reflection on this week's assigned readings.",
				DateCreated = Instant.FromUtc(2016, 11, 1, 06, 0, 0),
				DateToActivate = Instant.FromUtc(2016, 11, 10, 11, 59, 59),
				DateToShow = Instant.FromUtc(2016, 11, 10, 11, 59, 59),
				DateToStartOn = Instant.FromUtc(2016, 11, 10, 11, 59, 59),
				DateToSubmit = Instant.FromUtc(2016, 11, 14, 11, 59, 59),
				MaxAttempts = 2,
				MaxSubmits = 2,
				MaxScore = 50.0
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityAssignableDigitalResource");
		}

		[Test]
		public void EntityAttempt_MatchesReferenceJson() {

			var entity = new Attempt(
				"https://example.edu/terms/201601/courses/7/sections/1/assess/1/users/554433/attempts/1", defaultContextV1p1) {

				Assignable = new Assessment("https://example.edu/terms/201601/courses/7/sections/1/assess/1", defaultContextV1p1) { HideCaliperContext = true },
				Assignee = new Person("https://example.edu/users/554433", defaultContextV1p1) { HideCaliperContext = true },
				Count = 1,
				DateCreated = Instant.FromUtc(2016, 11, 15, 10, 05, 00),
				StartedAtTime = Instant.FromUtc(2016, 11, 15, 10, 05, 00),
				EndedAtTime = Instant.FromUtc(2016, 11, 15, 10, 55, 30),
				Duration = Period.FromMinutes(50) + Period.FromSeconds(30)
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityAttempt");
		}

		[Test]
		public void EntityAudioObject_MatchesReferenceJson() {

			var entity = new AudioObject(
				"https://example.edu/audio/765", defaultContextV1p1) {
				Name = "Audio Recording: IMS Caliper Sensor API Q&A.",
				MediaType = "audio/ogg",
				DatePublished = Instant.FromUtc(2016, 12, 01, 06, 00, 00),
				Duration = Period.FromMinutes(55) + Period.FromSeconds(13)
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityAudioObject");
		}

		[Test]
		public void EntityBookmarkAnnotation_MatchesReferenceJson() {

			var entity = new BookmarkAnnotation(
				"https://example.com/users/554433/texts/imscaliperimplguide/bookmarks/1", defaultContextV1p1) {
				Annotator = new Person("https://example.edu/users/554433", defaultContextV1p1) { HideCaliperContext = true },
				Annotated = new Page("https://example.com/#/texts/imscaliperimplguide/cfi/6/10!/4/2/2/2@0:0", defaultContextV1p1) { HideCaliperContext = true },
				BookmarkNotes = "Caliper profiles model discrete learning activities or supporting activities that facilitate learning.",
				DateCreated = Instant.FromUtc(2016, 8, 1, 6, 0, 0)
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityBookmarkAnnotation");
		}

		[Test]
		public void EntityChapter_MatchesReferenceJson() {

			var entity = new Chapter(
				"https://example.com/#/texts/imscaliperimplguide/cfi/6/10", defaultContextV1p1) {
				Name = "The Caliper Information Model",
				IsPartOf = new Document("https://example.com/#/texts/imscaliperimplguide", defaultContextV1p1) {
					DateCreated = Instant.FromUtc(2016, 10, 01, 6, 00, 00),
					Name = "IMS Caliper Implementation Guide",
					Version = "1.1",
                    HideCaliperContext = true
				}
			};
			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityChapter");
		}


		[Test]
		public void EntityCourseOffering_MatchesReferenceJson() {

			var entity = new CourseOffering(
					"https://example.edu/terms/201601/courses/7", defaultContextV1p1) {
				CourseNumber = "CPS 435",
				AcademicSession = "Fall 2016",
				Name = "CPS 435 Learning Analytics",
				DateCreated = CaliperTestEntities.Instant20160801060000,
				DateModified = Instant.FromUtc(2016, 09, 02, 11, 30, 00)
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityCourseOffering");
		}

		[Test]
		public void EntityCourseSection_MatchesReferenceJson() {

			var entity = new CourseSection(
					"https://example.edu/terms/201601/courses/7/sections/1", defaultContextV1p1) {
				AcademicSession = "Fall 2016",
				CourseNumber = "CPS 435-01",
				Name = "CPS 435 Learning Analytics, Section 01",
				Category = "seminar",
				SubOrganizationOf = new CourseOffering("https://example.edu/terms/201601/courses/7", defaultContextV1p1) {
					CourseNumber = "CPS 435",
                    HideCaliperContext = true
				},
				DateCreated = CaliperTestEntities.Instant20160801060000
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityCourseSection");
		}


		public static DigitalResource DigitalResourceSyllabusPDF = new DigitalResource(
					"https://example.edu/terms/201601/courses/7/sections/1/resources/1/syllabus.pdf", defaultContextV1p1) {
			Name = "Course Syllabus",
			MediaType = "application/pdf",
			Creators = new[] { new Person("https://example.edu/users/223344", defaultContextV1p1) },
			IsPartOf = new DigitalResourceCollection(
					"https://example.edu/terms/201601/courses/7/sections/1/resources/1", defaultContextV1p1) {
				Name = "Course Assets",
				IsPartOf = new CourseSection("https://example.edu/terms/201601/courses/7/sections/1", defaultContextV1p1)
			},
			DateCreated = Instant.FromUtc(2016, 08, 02, 11, 32, 00)
		};

		[Test]
		public void EntityDigitalResourceCollection_MatchesReferenceJson() {

			var entity = new DigitalResourceCollection(
					"https://example.edu/terms/201601/courses/7/sections/1/resources/2", defaultContextV1p1) {
				Name = "Video Collection",
				Keywords = new[] { "collection", "videos" },
				Items = new[] {
					new VideoObject("https://example.edu/videos/1225", defaultContextV1p1){
						MediaType = "video/ogg",
						Name = "Introduction to IMS Caliper",
						DateCreated = Instant.FromUtc(2016,08,01,06,00,00),
						Duration = Period.FromHours(1) + Period.FromMinutes(12) + Period.FromSeconds(27),
						Version = "1.1",
                        HideCaliperContext = true
                    },
					new VideoObject("https://example.edu/videos/5629", defaultContextV1p1){
						MediaType = "video/ogg",
						Name = "IMS Caliper Activity Profiles",
						DateCreated = Instant.FromUtc(2016,08,01,06,00,00),
						Duration = Period.FromMinutes(55) + Period.FromSeconds(13),
						Version = "1.1.1",
                        HideCaliperContext = true
                    }
				},
				IsPartOf = new CourseSection("https://example.edu/terms/201601/courses/7/sections/1", defaultContextV1p1) {
					SubOrganizationOf = new CourseOffering("https://example.edu/terms/201601/courses/7", defaultContextV1p1) { HideCaliperContext = true },
                    HideCaliperContext = true
				},
				DateCreated = CaliperTestEntities.Instant20160801060000,
				DateModified = Instant.FromUtc(2016, 09, 02, 11, 30, 00)
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityDigitalResourceCollection");
		}

		[Test]
		public void EntityDocument_MatchesReferenceJson() {

			var entity = new Document(
					"https://example.edu/etexts/201.epub", defaultContextV1p1) {
				Name = "IMS Caliper Implementation Guide",
				MediaType = "application/epub+zip",
				Creators = new[] {
					new Person("https://example.edu/people/12345", defaultContextV1p1) { HideCaliperContext = true },
					new Person("https://example.com/staff/56789", defaultContextV1p1) { HideCaliperContext = true }
                },
				DateCreated = CaliperTestEntities.Instant20160801060000,
				DatePublished = Instant.FromUtc(2016, 10, 01, 06, 00, 00),
				Version = "1.1"
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityDocument");
		}


		[Test]
		public void EntityFillInBlankResponse_MatchesReferenceJson() {

			var entity = new FillInBlankResponse(
				"https://example.edu/terms/201601/courses/7/sections/1/assess/1/items/1/users/554433/responses/1", defaultContextV1p1) {
				Attempt = new Attempt("https://example.edu/terms/201601/courses/7/sections/1/assess/1/items/1/users/554433/attempts/1", defaultContextV1p1) {
					Assignee = new Person("https://example.edu/users/554433", defaultContextV1p1) { HideCaliperContext = true },
					Assignable = new AssessmentItem("https://example.edu/terms/201601/courses/7/sections/1/assess/1/items/1", defaultContextV1p1) {
						IsPartOf = new Assessment("https://example.edu/terms/201601/courses/7/sections/1/assess/1", defaultContextV1p1) { HideCaliperContext = true },
                        HideCaliperContext = true
                    },
					Count = 1,
					StartedAtTime = Instant.FromUtc(2016, 11, 15, 10, 15, 02),
					EndedAtTime = Instant.FromUtc(2016, 11, 15, 10, 15, 12),
                    HideCaliperContext = true
				},

				DateCreated = Instant.FromUtc(2016, 11, 15, 10, 15, 12),
				StartedAtTime = Instant.FromUtc(2016, 11, 15, 10, 15, 02),
				EndedAtTime = Instant.FromUtc(2016, 11, 15, 10, 15, 12),
				Values = new[] { "data interoperability", "semantic interoperability" }
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityFillinBlankResponse");
		}


		[Test]
		public void EntityForum_MatchesReferenceJson() {

			var entity = new Forum(
				"https://example.edu/terms/201601/courses/7/sections/1/forums/1", defaultContextV1p1) {
				Name = "Caliper Forum",
				Items = new[] {
					new Thread("https://example.edu/terms/201601/courses/7/sections/1/forums/1/topics/1", defaultContextV1p1){
						Name = "Caliper Information Model",
						DateCreated = Instant.FromUtc(2016,11,01,09,30,00),
                        HideCaliperContext = true
					},
					new Thread("https://example.edu/terms/201601/courses/7/sections/1/forums/1/topics/2", defaultContextV1p1){
						Name = "Caliper Sensor API",
						DateCreated = Instant.FromUtc(2016,11,01,09,30,00),
                        HideCaliperContext = true
                    },
					new Thread("https://example.edu/terms/201601/courses/7/sections/1/forums/1/topics/3", defaultContextV1p1){
						Name = "Caliper Certification",
						DateCreated = Instant.FromUtc(2016,11,01,09,30,00),
                        HideCaliperContext = true
                    }
				},
				IsPartOf = new CourseSection("https://example.edu/terms/201601/courses/7/sections/1", defaultContextV1p1) {
					SubOrganizationOf = new CourseOffering("https://example.edu/terms/201601/courses/7", defaultContextV1p1) { HideCaliperContext = true },
                    HideCaliperContext = true
                },

				DateCreated = Instant.FromUtc(2016, 08, 01, 6, 0, 0),
				DateModified = Instant.FromUtc(2016, 09, 02, 11, 30, 0)
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityForum");
		}

		[Test]
		public void EntityGroup_MatchesReferenceJson() {
			var entity = new Group("https://example.edu/terms/201601/courses/7/sections/1/groups/2", defaultContextV1p1) {
				Name = "Discussion Group 2",
				SubOrganizationOf = new CourseSection("https://example.edu/terms/201601/courses/7/sections/1", defaultContextV1p1) {
					SubOrganizationOf = new CourseOffering("https://example.edu/terms/201601/courses/7", defaultContextV1p1) { HideCaliperContext = true },
                    HideCaliperContext = true
                },
				Members = new[] {
					new Person("https://example.edu/users/554433", defaultContextV1p1){ HideCaliperContext = true },
					new Person("https://example.edu/users/778899", defaultContextV1p1) { HideCaliperContext = true },
					new Person("https://example.edu/users/445566", defaultContextV1p1) { HideCaliperContext = true },
					new Person("https://example.edu/users/667788", defaultContextV1p1) { HideCaliperContext = true },
					new Person("https://example.edu/users/889900", defaultContextV1p1) { HideCaliperContext = true }
				},
				DateCreated = CaliperTestEntities.Instant20161101060000,
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityGroup");
		}

		[Test]
		public void EntityFrame_MatchesReferenceJson() {

			var entity = new Frame(
				"https://example.edu/etexts/201?index=2502", defaultContextV1p1) {
				DateCreated = Instant.FromUtc(2016, 08, 01, 6, 0, 0),
				Index = 2502,
				IsPartOf = new Document("https://example.edu/etexts/201", defaultContextV1p1) {
					Name = "IMS Caliper Implementation Guide",
					Version = "1.1",
                    HideCaliperContext = true
				}
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityFrame");
		}


		[Test]
		public void EntityHighlightAnnotation_MatchesReferenceJson() {

			var entity = new HighlightAnnotation(
				"https://example.edu/users/554433/etexts/201/highlights/20", defaultContextV1p1) {
				Annotator = CaliperTestEntities.Person554433(defaultContextV1p1),
				Annotated = new Document("https://example.edu/etexts/201", defaultContextV1p1) { HideCaliperContext = true },
				Selection = new TextPositionSelector() {
					Start = 2300,
					End = 2370
				},
				SelectionText = "ISO 8601 formatted date and time expressed with millisecond precision.",
				DateCreated = CaliperTestEntities.Instant20160801060000
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityHighlightAnnotation");
		}



		[Test]
		public void EntityImageObject_MatchesReferenceJson() {

			var entity = new ImageObject(
				"https://example.edu/images/caliper_lti.jpg", defaultContextV1p1) {
				Name = "IMS Caliper/LTI Integration Work Flow",
				MediaType = "image/jpeg",
				DateCreated = Instant.FromUtc(2016, 09, 01, 6, 0, 0)
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityImageObject");
		}


		[Test]
		public void EntityLearningObjective_MatchesReferenceJson() {

			var entity = new AssignableDigitalResource(
				"https://example.edu/terms/201601/courses/7/sections/1/assign/2", defaultContextV1p1) {
				Name = "Caliper Profile Design",
				Description = "Choose a learning activity and describe the actions, entities and events that comprise it.",
				LearningObjectives = new[] {
					new LearningObjective("https://example.edu/terms/201601/courses/7/sections/1/objectives/1", defaultContextV1p1) {
						Name = "Research techniques",
						Description = "Demonstrate ability to model a learning activity as a Caliper profile.",
						DateCreated = Instant.FromUtc(2016,08,01,06,00,00),
                        HideCaliperContext = true
                    }
				},

				DateToActivate = Instant.FromUtc(2016, 11, 10, 11, 59, 59),
				DateToShow = Instant.FromUtc(2016, 11, 10, 11, 59, 59),
				DateCreated = Instant.FromUtc(2016, 11, 01, 06, 00, 00),
				DateToStartOn = Instant.FromUtc(2016, 11, 15, 11, 59, 59),
				DateToSubmit = Instant.FromUtc(2016, 11, 14, 11, 59, 59),
				MaxAttempts = 2,
				MaxSubmits = 2,
				MaxScore = 50.0
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityLearningObjective");
		}

		[Test]
		public void EntityLtiSession_MatchesReferenceJson() {

            var entity = new LtiSession(
                "https://example.edu/lti/sessions/b533eb02823f31024e6b7f53436c42fb99b31241", defaultContextV1p1)
            {
                Context = defaultContextV1p1.Value,
                User = new Person("https://example.edu/users/554433", defaultContextV1p1) { HideCaliperContext = true },
                MessageParameters = new CaliperTestEntities.LtiParamsLtiSession(),
                DateCreated = CaliperTestEntities.Instant20181115101500,
                StartedAt = CaliperTestEntities.Instant20181115101500

            };

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityLtiSession");
		}


		[Test]
		public void EntityMediaObject_MatchesReferenceJson() {

			var entity = new MediaObject(
				"https://example.edu/media/54321", defaultContextV1p1) {
				DateCreated = Instant.FromUtc(2016, 08, 1, 6, 0, 0),
				DateModified = Instant.FromUtc(2016, 09, 2, 11, 30, 0),
				Duration = Period.FromHours(1) + Period.FromMinutes(17) + Period.FromSeconds(50)
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityMediaObject");
		}


		[Test]
		public void EntityMediaLocation_MatchesReferenceJson() {

			var entity = new MediaLocation(
				"https://example.edu/videos/1225", defaultContextV1p1) {
				CurrentTime = Period.FromMinutes(30) + Period.FromSeconds(54),
				DateCreated = Instant.FromUtc(2016, 08, 1, 6, 0, 0)
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityMediaLocation");
		}

		[Test]
		public void EntityMembership_MatchesReferenceJson() {

			var entity = new Membership(
				"https://example.edu/terms/201601/courses/7/sections/1/rosters/1/members/554433", defaultContextV1p1) {
				Member = new Person("https://example.edu/users/554433", defaultContextV1p1) { HideCaliperContext = true },
				Organization = new CourseSection("https://example.edu/terms/201601/courses/7/sections/1", defaultContextV1p1) {
					SubOrganizationOf = new CourseOffering("https://example.edu/terms/201601/courses/7", defaultContextV1p1) { HideCaliperContext = true },
                    HideCaliperContext = true
                },
				Roles = new[] { Role.Learner },
				Status = Status.Active,
				DateCreated = Instant.FromUtc(2016, 11, 1, 6, 0, 0)
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityMembership");
		}


		[Test]
		public void EntityMessage_MatchesReferenceJson() {

			var entity = new Message(
				"https://example.edu/terms/201601/courses/7/sections/1/forums/2/topics/1/messages/3", defaultContextV1p1) {
				Creators = new[] { new Person("https://example.edu/users/778899", defaultContextV1p1) { HideCaliperContext = true } },
				Body = "The Caliper working group provides a set of Caliper Sensor reference implementations for"
					+ " the purposes of education and experimentation.  They have not been tested for use in a "
					+ "production environment.  See the Caliper Implementation Guide for more details.",
				ReplyTo = new Message("https://example.edu/terms/201601/courses/7/sections/1/forums/2/topics/1/messages/2", defaultContextV1p1) { HideCaliperContext = true },
				IsPartOf = new Thread("https://example.edu/terms/201601/courses/7/sections/1/forums/2/topics/1", defaultContextV1p1) {
					IsPartOf = new Forum("https://example.edu/terms/201601/courses/7/sections/1/forums/2", defaultContextV1p1) { HideCaliperContext = true },
                    HideCaliperContext = true
                },
				Attachments = new[] { new Document("https://example.edu/etexts/201.epub", defaultContextV1p1) {
						Name = "IMS Caliper Implementation Guide",
						DateCreated = Instant.FromUtc(2016,10,01,06,00,00),
						Version = "1.1",
                        HideCaliperContext = true
                    }
				},
				DateCreated = Instant.FromUtc(2016, 11, 15, 10, 15, 30)
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityMessage");

		}

		[Test]
		public void EntityMultipleChoiceResponse_MatchesReferenceJson() {

			var entity = new MultipleChoiceResponse(
				"https://example.edu/terms/201601/courses/7/sections/1/assess/1/items/2/users/554433/responses/1", defaultContextV1p1) {

				Attempt = new Attempt("https://example.edu/terms/201601/courses/7/sections/1/assess/1/items/2/users/554433/attempts/1", defaultContextV1p1) {
					Assignee = new Person("https://example.edu/users/554433", defaultContextV1p1) { HideCaliperContext = true },
					Assignable = new AssessmentItem("https://example.edu/terms/201601/courses/7/sections/1/assess/1/items/2", defaultContextV1p1) {
						IsPartOf = new Assessment("https://example.edu/terms/201601/courses/7/sections/1/assess/1", defaultContextV1p1) { HideCaliperContext = true },
                        HideCaliperContext = true
                    },
					Count = 1,
					StartedAtTime = Instant.FromUtc(2016, 11, 15, 10, 15, 14),
					EndedAtTime = Instant.FromUtc(2016, 11, 15, 10, 15, 20),
                    HideCaliperContext = true

				},
				DateCreated = Instant.FromUtc(2016, 11, 15, 10, 15, 20),
				StartedAtTime = Instant.FromUtc(2016, 11, 15, 10, 15, 14),
				EndedAtTime = Instant.FromUtc(2016, 11, 15, 10, 15, 20),
				Value = "C"
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityMultipleChoiceResponse");
		}

		[Test]
		public void EntityMultipleResponseResponse_MatchesReferenceJson() {

			var entity = new MultipleResponseResponse(
				"https://example.edu/terms/201601/courses/7/sections/1/assess/1/items/3/users/554433/responses/1", defaultContextV1p1) {
				Attempt = new Attempt("https://example.edu/terms/201601/courses/7/sections/1/assess/1/items/3/users/554433/attempts/1", defaultContextV1p1) {
					Assignee = new Person("https://example.edu/users/554433", defaultContextV1p1) { HideCaliperContext = true },
					Assignable = new AssessmentItem("https://example.edu/terms/201601/courses/7/sections/1/assess/1/items/3", defaultContextV1p1) {
						IsPartOf = new Assessment("https://example.edu/terms/201601/courses/7/sections/1/assess/1", defaultContextV1p1) { HideCaliperContext = true },
                        HideCaliperContext = true
                    },
					Count = 1,
					StartedAtTime = Instant.FromUtc(2016, 11, 15, 10, 15, 22),
					EndedAtTime = Instant.FromUtc(2016, 11, 15, 10, 15, 30),
                    HideCaliperContext = true
                },
				DateCreated = Instant.FromUtc(2016, 11, 15, 10, 15, 22),
				StartedAtTime = Instant.FromUtc(2016, 11, 15, 10, 15, 22),
				EndedAtTime = Instant.FromUtc(2016, 11, 15, 10, 15, 30),
				Values = new[] { "A", "D", "E" }
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityMultipleResponseResponse");
		}

		[Test]
		public void EntityOrganization_MatchesReferenceJson() {

			var entity = new Organization(
							"https://example.edu/colleges/1/depts/1", defaultContextV1p1) {
				Name = "Computer Science Department",
				SubOrganizationOf = new Organization("https://example.edu/colleges/1", defaultContextV1p1) {
					Name = "College of Engineering",
                    HideCaliperContext = true
                }
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityOrganization");
		}


		[Test]
		public void EntityPage_MatchesReferenceJson() {

			var entity = new Page(
				"https://example.com/#/texts/imscaliperimplguide/cfi/6/10!/4/2/2/2@0:0", defaultContextV1p1) {
				Name = "Page 5",
				IsPartOf = new Chapter("https://example.com/#/texts/imscaliperimplguide/cfi/6/10", defaultContextV1p1) {
					Name = "Chapter 1",
					IsPartOf = new Document("https://example.com/#/texts/imscaliperimplguide", defaultContextV1p1) {
						Name = "IMS Caliper Implementation Guide",
						DateCreated = Instant.FromUtc(2016, 10, 01, 06, 00, 00),
						Version = "1.1",
                        HideCaliperContext = true
					},
                    HideCaliperContext = true
                }
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityPage");
		}


		[Test]
		public void EntityPerson_MatchesReferenceJson() {

			var entity = new Person(
				"https://example.edu/users/554433", defaultContextV1p1) {
				DateCreated = CaliperTestEntities.Instant20160801060000,
				DateModified = Instant.FromUtc(2016, 09, 02, 11, 30, 00)
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityPerson");
		}

		[Test]
		public void EntityResponseExtended_MatchesReferenceJson() {
			var entity = new Response(
				"https://example.edu/terms/201601/courses/7/sections/1/assess/1/items/6/users/554433/responses/1", defaultContextV1p1) {
				Attempt = new Attempt("https://example.edu/terms/201601/courses/7/sections/1/assess/1/items/6/users/554433/attempts/1", defaultContextV1p1) {
					Assignee = CaliperTestEntities.Person554433(defaultContextV1p1),
					Assignable = CaliperTestEntities.AssessmentItem6(defaultContextV1p1),
					Count = 1,
					StartedAtTime = CaliperTestEntities.Instant20161115101546,
					EndedAtTime = CaliperTestEntities.Instant20161115101720,
                    HideCaliperContext = true
				},
				DateCreated = CaliperTestEntities.Instant20161115101546,
				StartedAtTime = CaliperTestEntities.Instant20161115101546,
				EndedAtTime = CaliperTestEntities.Instant20161115101720,
				Extensions = new { value = "A Caliper Event describes a relationship established between an actor and an object.  The relationship is formed as a result of a purposeful action undertaken by the actor in connection to the object at a particular moment. A learner starting an assessment, annotating a reading, pausing a video, or posting a message to a forum, are examples of learning activities that Caliper models as events." }
			};

            entity.Attempt.Assignable.HideCaliperContext = true;
            entity.Attempt.Assignable.IsPartOf.HideCaliperContext = true;

            var coerced = JsonAssertions.coerce(entity,
				new string[] { "..attempt.assignee" });

			JsonAssertions.AssertSameObjectJson(coerced, "caliperEntityResponseExtended");
		}


		[Test]
		public void EntityResult_MatchesReferenceJson() {

			var entity = new Result(
				"https://example.edu/terms/201601/courses/7/sections/1/assess/1/users/554433/attempts/1/results/1", defaultContextV1p1) {
				Attempt = new Attempt("https://example.edu/terms/201601/courses/7/sections/1/assess/1/users/554433/attempts/1", defaultContextV1p1) {
					Assignee = new Person("https://example.edu/users/554433", defaultContextV1p1) { HideCaliperContext = true },
					Assignable = new Assessment("https://example.edu/terms/201601/courses/7/sections/1/assess/1", defaultContextV1p1) { HideCaliperContext = true },
					Count = 1,
					DateCreated = Instant.FromUtc(2016, 11, 15, 10, 05, 00),
					StartedAtTime = Instant.FromUtc(2016, 11, 15, 10, 05, 00),
					EndedAtTime = Instant.FromUtc(2016, 11, 15, 10, 55, 30),
					Duration = Period.FromMinutes(50) + Period.FromSeconds(30),
                    HideCaliperContext = true
				},
				Comment = "Consider retaking the assessment.",
				MaxResultScore = 15.0,
				ResultScore = 10.0,
				ScoredBy = new SoftwareApplication("https://example.edu/autograder", defaultContextV1p1) {
					DateCreated = Instant.FromUtc(2016, 11, 15, 10, 55, 58),
                    HideCaliperContext = true
                },
				DateCreated = Instant.FromUtc(2016, 11, 15, 10, 56, 00)
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityResult");
		}

		[Test]
		public void EntityScore_MatchesReferenceJson() {
			var entity = new Score("https://example.edu/terms/201601/courses/7/sections/1/assess/1/users/554433/attempts/1/scores/1", defaultContextV1p1) {
				Attempt = new Attempt("https://example.edu/terms/201601/courses/7/sections/1/assess/1/users/554433/attempts/1", defaultContextV1p1) {
					Assignee = CaliperTestEntities.Person554433(defaultContextV1p1),
					Assignable = new Assessment("https://example.edu/terms/201601/courses/7/sections/1/assess/1", defaultContextV1p1),
					Count = 1,
					DateCreated = Instant.FromUtc(2016, 11, 15, 10, 05, 00),
					StartedAtTime = Instant.FromUtc(2016, 11, 15, 10, 05, 00),
					EndedAtTime = Instant.FromUtc(2016, 11, 15, 10, 55, 30),
					Duration = Period.FromMinutes(50) + Period.FromSeconds(30),
                    HideCaliperContext = true
				},
				MaxScore = 15.0,
				ScoreGiven = 10.0,
				ScoredBy = new SoftwareApplication("https://example.edu/autograder", defaultContextV1p1) {
					DateCreated = Instant.FromUtc(2016, 11, 15, 10, 55, 58),
                    HideCaliperContext = true
                },
				Comment = "auto-graded exam",
				DateCreated = Instant.FromUtc(2016, 11, 15, 10, 56, 00),
			};

			var coerced = JsonAssertions.coerce(entity,
				new string[] { "..attempt.assignee", "..attempt.assignable" });


			JsonAssertions.AssertSameObjectJson(coerced, "caliperEntityScore");
		}


		[Test]
		public void EntitySelectTextResponse_MatchesReferenceJson() {

			var entity = new SelectTextResponse(
				"https://example.edu/terms/201601/courses/7/sections/1/assess/1/items/4/users/554433/responses/1", defaultContextV1p1) {
				Attempt = new Attempt("https://example.edu/terms/201601/courses/7/sections/1/assess/1/items/4/users/554433/attempts/1", defaultContextV1p1) {
					Assignee = new Person("https://example.edu/users/554433", defaultContextV1p1) { HideCaliperContext = true },
					Assignable = new AssessmentItem("https://example.edu/terms/201601/courses/7/sections/1/assess/1/items/4", defaultContextV1p1) {
						IsPartOf = new Assessment("https://example.edu/terms/201601/courses/7/sections/1/assess/1", defaultContextV1p1) { HideCaliperContext = true },
                        HideCaliperContext = true
                    },
					Count = 1,
					StartedAtTime = Instant.FromUtc(2016, 11, 15, 10, 15, 32),
					EndedAtTime = Instant.FromUtc(2016, 11, 15, 10, 15, 38),
                    HideCaliperContext = true
				},

				DateCreated = Instant.FromUtc(2016, 11, 15, 10, 15, 32),
				StartedAtTime = Instant.FromUtc(2016, 11, 15, 10, 15, 32),
				EndedAtTime = Instant.FromUtc(2016, 11, 15, 10, 15, 38),
				Values = new[] { "Information Model", "Sensor API", "Profiles" }
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntitySelectTextResponse");
		}


		[Test]
		public void EntitySession_MatchesReferenceJson() {

			var entity = new Session(
				"https://example.edu/sessions/1f6442a482de72ea6ad134943812bff564a76259", defaultContextV1p1) {
				User = new Person("https://example.edu/users/554433", defaultContextV1p1) { HideCaliperContext = true },
				StartedAt = Instant.FromUtc(2016, 9, 15, 10, 00, 00)
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntitySession");
		}

		[Test]
		public void EntitySharedAnnotation_MatchesReferenceJson() {

			var entity = new ShareAnnotation(
				"https://example.edu/users/554433/etexts/201/shares/1", defaultContextV1p1) {
				Annotator = new Person("https://example.edu/users/554433", defaultContextV1p1) { HideCaliperContext = true },
				Annotated = new Document("https://example.edu/etexts/201.epub", defaultContextV1p1) { HideCaliperContext = true },
				WithAgents = new[] {
					new Person("https://example.edu/users/657585", defaultContextV1p1) { HideCaliperContext = true },
					new Person("https://example.edu/users/667788", defaultContextV1p1) { HideCaliperContext = true }
                },
				DateCreated = Instant.FromUtc(2016, 08, 01, 9, 00, 00)
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntitySharedAnnotation");
		}

		[Test]
		public void EntitySoftwareApplication_MatchesReferenceJson() {

			var entity = new SoftwareApplication(
				"https://example.edu/autograder", defaultContextV1p1) {
				Name = "Auto Grader",
				Description = "Automates assignment scoring.",
				Version = "2.5.2"
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntitySoftwareApplication");
		}

		[Test]
		public void EntityTagAnnotation_MatchesReferenceJson() {

			var entity = new TagAnnotation(
				"https://example.com/users/554433/texts/imscaliperimplguide/tags/3", defaultContextV1p1) {
				Annotator = new Person("https://example.edu/users/554433", defaultContextV1p1) { HideCaliperContext = true },
				Annotated = new Page("https://example.com/#/texts/imscaliperimplguide/cfi/6/10!/4/2/2/2@0:0", defaultContextV1p1) { HideCaliperContext = true },
				Tags = new[] { "profile", "event", "entity" },
				DateCreated = Instant.FromUtc(2016, 08, 01, 9, 0, 0)
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityTagAnnotation");
		}

		[Test]
		public void EntityThread_MatchesReferenceJson() {

			var msg1 = new Message(
                "https://example.edu/terms/201601/courses/7/sections/1/forums/1/topics/1/messages/1", defaultContextV1p1) { HideCaliperContext = true };
			var msg2 = new Message(
				"https://example.edu/terms/201601/courses/7/sections/1/forums/1/topics/1/messages/2", defaultContextV1p1) { ReplyTo = msg1, HideCaliperContext = true };
			var msg3 = new Message(
				"https://example.edu/terms/201601/courses/7/sections/1/forums/1/topics/1/messages/3", defaultContextV1p1) {
				ReplyTo = new Message("https://example.edu/terms/201601/courses/7/sections/1/forums/1/topics/1/messages/2", defaultContextV1p1) { HideCaliperContext = true },
                HideCaliperContext = true
            };

			var entity = new Thread(
				"https://example.edu/terms/201601/courses/7/sections/1/forums/1/topics/1", defaultContextV1p1) {
				Name = "Caliper Information Model",
				Items = new[] { msg1, msg2, msg3 },
				IsPartOf = new Forum("https://example.edu/terms/201601/courses/7/sections/1/forums/1", defaultContextV1p1) {
					Name = "Caliper Forum",
					IsPartOf = new CourseSection("https://example.edu/terms/201601/courses/7/sections/1", defaultContextV1p1) {
						SubOrganizationOf = new CourseOffering("https://example.edu/terms/201601/courses/7", defaultContextV1p1) { HideCaliperContext = true },
                        HideCaliperContext = true
                    },
                    HideCaliperContext = true
                },
				DateCreated = CaliperTestEntities.Instant20160801060000,
				DateModified = Instant.FromUtc(2016, 09, 02, 11, 30, 00)
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityThread");
		}

		[Test]
		public void EntityTrueFalseResponse_MatchesReferenceJson() {

			var entity = new TrueFalseResponse(
				"https://example.edu/terms/201601/courses/7/sections/1/assess/1/items/5/users/554433/responses/1", defaultContextV1p1) {
				Attempt = new Attempt("https://example.edu/terms/201601/courses/7/sections/1/assess/1/items/5/users/554433/attempts/1", defaultContextV1p1) {
					Assignee = new Person("https://example.edu/users/554433", defaultContextV1p1) { HideCaliperContext = true },
					Assignable = new AssessmentItem("https://example.edu/terms/201601/courses/7/sections/1/assess/1/items/5", defaultContextV1p1) {
						IsPartOf = new Assessment("https://example.edu/terms/201601/courses/7/sections/1/assess/1", defaultContextV1p1) { HideCaliperContext = true },
                        HideCaliperContext = true
                    },
					Count = 1,
					StartedAtTime = Instant.FromUtc(2016, 11, 15, 10, 15, 40),
					EndedAtTime = Instant.FromUtc(2016, 11, 15, 10, 15, 45),
                    HideCaliperContext = true
				},
				DateCreated = Instant.FromUtc(2016, 11, 15, 10, 15, 45),
				StartedAtTime = Instant.FromUtc(2016, 11, 15, 10, 15, 40),
				EndedAtTime = Instant.FromUtc(2016, 11, 15, 10, 15, 45),
				Value = "true"

			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityTrueFalseResponse");
		}


		[Test]
		public void EntityVideoObject_MatchesReferenceJson() {

			var entity = new VideoObject(
				"https://example.edu/videos/1225", defaultContextV1p1) {
				MediaType = "video/ogg",
				Name = "Introduction to IMS Caliper",
				Version = "1.1",
				DateCreated = CaliperTestEntities.Instant20160801060000,
				DateModified = Instant.FromUtc(2016, 09, 02, 11, 30, 00),
				Duration = Period.FromHours(1) + Period.FromMinutes(12) + Period.FromSeconds(27)

			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityVideoObject");
		}

		[Test]
		public void EntityWebPage_MatchesReferenceJson() {

			var entity = new WebPage(
				"https://example.edu/terms/201601/courses/7/sections/1/pages/index.html", defaultContextV1p1) {
				Name = "CPS 435-01 Landing Page",
				MediaType = "text/html",
				IsPartOf = new CourseSection("https://example.edu/terms/201601/courses/7/sections/1", defaultContextV1p1) {
					CourseNumber = "CPS 435-01",
					AcademicSession = "Fall 2016",
                    HideCaliperContext = true
				}
			};

			JsonAssertions.AssertSameObjectJson(entity, "caliperEntityWebPage");
		}

		[Test]
		public void EnvelopeEntityBatch_MatchesReferenceJson() {

			var Person554433 = CaliperTestEntities.Person554433dates(defaultContextV1p1);

			var Epub201 = new Document("https://example.edu/etexts/201.epub", defaultContextV1p1) {
				Name = "IMS Caliper Implementation Guide",
				Creators = new[] { new Person("https://example.edu/people/12345", defaultContextV1p1){ HideCaliperContext = true },
					new Person("https://example.com/staff/56789", defaultContextV1p1) { HideCaliperContext = true } },
				DateCreated = CaliperTestEntities.Instant20161001060000,
				Version = "1.1"
			};

			var VideoCollection = new DigitalResourceCollection(
				"https://example.edu/terms/201601/courses/7/sections/1/resources/2", defaultContextV1p1) {
				Name = "Video Collection",
				IsPartOf = new CourseSection("https://example.edu/terms/201601/courses/7/sections/1", defaultContextV1p1) {
					SubOrganizationOf = new CourseOffering("https://example.edu/terms/201601/courses/7", defaultContextV1p1) { HideCaliperContext = true },
                    HideCaliperContext = true
                },
				Items = new[] { CaliperTestEntities.VideoObject_1(defaultContextV1p1), CaliperTestEntities.VideoObject_2(defaultContextV1p1) },
				DateCreated = CaliperTestEntities.Instant20160801060000,
				DateModified = CaliperTestEntities.Instant20160902113000
			};

            foreach(var item in VideoCollection.Items)
            {
                item.HideCaliperContext = true;
            }

			var envelope = new CaliperMessage<JObject>(defaultContextV1p1) {
				SensorId = CaliperTestEntities.EnvelopeDefaultSensorId,
				SendTime = CaliperTestEntities.EnvelopeDefaultSendTime,
				Data = new[] {
					clean(toJobject(Person554433)),
					clean(toJobject(Epub201)),
					clean(toJobject(VideoCollection))
				}
			};

			JsonAssertions.AssertSameObjectJson(envelope, "caliperEnvelopeEntityBatch", false);
		}

		[Test]
		public void EnvelopeEntitySingle_MatchesReferenceJson() {

            var digitalResource = CaliperTestEntities.DigitalResourceSyllabusPDF(defaultContextV1p1);
            digitalResource.Creators[0].HideCaliperContext = true;
            digitalResource.IsPartOf.HideCaliperContext = true;
            (digitalResource.IsPartOf as DigitalResourceCollection).IsPartOf.HideCaliperContext = true;

            var envelope = new CaliperMessage<Entity>(defaultContextV1p1)
            {
				SensorId = CaliperTestEntities.EnvelopeDefaultSensorId,
				SendTime = CaliperTestEntities.EnvelopeDefaultSendTime,
				Data = new[] { digitalResource }
			};

            JsonAssertions.AssertSameObjectJson(envelope, "caliperEnvelopeEntitySingle");
		}


		[Test]
		public void EnvelopeEventBatch_MatchesReferenceJson() {

			var NavigationEvent = new NavigationEvent(
				"urn:uuid:72f66ce5-d2ec-44cc-bce5-41602e1015dc", defaultContextV1p1) {
				Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
				Object = new WebPage("https://example.edu/terms/201601/courses/7/sections/1/pages/2", defaultContextV1p1) {
					Name = "Learning Analytics Specifications",
					Description = "Overview of Learning Analytics Specifications with particular emphasis on IMS Caliper.",
					DateCreated = CaliperTestEntities.Instant20160801090000,
                    HideCaliperContext = true
				},
				EventTime = CaliperTestEntities.Instant20161115101500,
				Referrer = new WebPage("https://example.edu/terms/201601/courses/7/sections/1/pages/1", defaultContextV1p1),
				EdApp = CaliperTestEntities.EpubReader123(defaultContextV1p1),
				Group = CaliperTestEntities.CourseSectionCPS43501Fall16(defaultContextV1p1),
				Membership = CaliperTestEntities.EntityMembership554433Learner(defaultContextV1p1),
				Session = CaliperTestEntities.Session6259(defaultContextV1p1)
            };
            NavigationEvent.Referrer.HideCaliperContext = true;
            NavigationEvent.EdApp.HideCaliperContext = true;
            (NavigationEvent.Group as CourseSection).HideCaliperContext = true;
            NavigationEvent.Membership.HideCaliperContext = true;
            NavigationEvent.Session.HideCaliperContext = true;

            var BookmarkAnnotation = new BookmarkAnnotation(
				"https://example.com/users/554433/texts/imscaliperimplguide/bookmarks/1", defaultContextV1p1) {
				Annotator = CaliperTestEntities.Person554433(defaultContextV1p1),
				Annotated = new Page("https://example.com/#/texts/imscaliperimplguide/cfi/6/10!/4/2/2/2@0:0", defaultContextV1p1),
				BookmarkNotes = "Caliper profiles model discrete learning activities or supporting activities that facilitate learning.",
				DateCreated = CaliperTestEntities.Instant20161115102000

			};

			var AnnotationEvent = new AnnotationEvent(
				"urn:uuid:c0afa013-64df-453f-b0a6-50f3efbe4cc0", BookmarkAnnotation, defaultContextV1p1) {
				Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
				Object = new Document("https://example.com/#/texts/imscaliperimplguide", defaultContextV1p1) {
					Name = "IMS Caliper Implementation Guide",
					Version = "1.1",
                    HideCaliperContext = true
                },
				EventTime = CaliperTestEntities.Instant20161115102000,
				EdApp = CaliperTestEntities.EpubReader123(defaultContextV1p1),
				Group = CaliperTestEntities.CourseSectionCPS43501Fall16(defaultContextV1p1),
				Membership = CaliperTestEntities.EntityMembership554433Learner(defaultContextV1p1),
				Session = CaliperTestEntities.Session6259(defaultContextV1p1)
            };
            AnnotationEvent.Generated.HideCaliperContext = true;
            AnnotationEvent.EdApp.HideCaliperContext = true;
            (AnnotationEvent.Group as CourseSection).HideCaliperContext = true;
            AnnotationEvent.Membership.HideCaliperContext = true;
            AnnotationEvent.Session.HideCaliperContext = true;

            var ViewEvent = new ViewEvent(
							"urn:uuid:94bad4bd-a7b1-4c3e-ade4-2253efe65172", defaultContextV1p1) {
				Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
				Object = CaliperTestEntities.Epub201(defaultContextV1p1),
				EventTime = CaliperTestEntities.Instant20161115102100,
				EdApp = CaliperTestEntities.EpubReader123(defaultContextV1p1),
				Group = CaliperTestEntities.CourseSectionCPS43501Fall16(defaultContextV1p1),
				Membership = CaliperTestEntities.EntityMembership554433Learner(defaultContextV1p1),
				Session = CaliperTestEntities.Session6259(defaultContextV1p1)
            };
            ViewEvent.Object.HideCaliperContext = true;
            ViewEvent.EdApp.HideCaliperContext = true;
            (ViewEvent.Group as CourseSection).HideCaliperContext = true;
            ViewEvent.Membership.HideCaliperContext = true;
            ViewEvent.Session.HideCaliperContext = true;

            var envelope = new CaliperMessage<JObject>(defaultContextV1p1)
            {
				SensorId = CaliperTestEntities.EnvelopeDefaultSensorId,
				SendTime = CaliperTestEntities.EnvelopeDefaultSendTime,
				Data = new[] {
					clean(toJobject(NavigationEvent)),
					clean(toJobject(AnnotationEvent)),
					clean(toJobject(ViewEvent))
				}
			};

			var coerced = JsonAssertions.coerce(envelope,
				new string[] { "..membership.member", "..membership.organization",
					"..generated.annotator", "..generated.annotated" });

			JsonAssertions.AssertSameObjectJson(coerced, "caliperEnvelopeEventBatch", false);
		}


		[Test]
		public void EnvelopeEventSingle_MatchesReferenceJson() {

			var envelope = new CaliperMessage<Event>(defaultContextV1p1)
            {
				SensorId = "https://example.edu/sensors/1",
				SendTime = CaliperTestEntities.EnvelopeDefaultSendTime,
				Data = new[] { new AssessmentEvent(
					"urn:uuid:c51570e4-f8ed-4c18-bb3a-dfe51b2cc594", Action.Started, defaultContextV1p1) {
						Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
						Object = CaliperTestEntities.AssessmentQuizOne(defaultContextV1p1),
						Generated = CaliperTestEntities.Attempt1c(defaultContextV1p1),
						EventTime = CaliperTestEntities.Instant20161115101500,
						EdApp = CaliperTestEntities.SoftwareAppV2(defaultContextV1p1),
						Group = CaliperTestEntities.CourseSectionCPS43501Fall16(defaultContextV1p1),
						Membership = CaliperTestEntities.EntityMembership554433Learner(defaultContextV1p1),
						Session = CaliperTestEntities.Session6259edu(defaultContextV1p1)
                    }
				}
			};
            envelope.Data.First().Object.HideCaliperContext = true;
            envelope.Data.First().Generated.HideCaliperContext = true;
            envelope.Data.First().EdApp.HideCaliperContext = true;
            (envelope.Data.First().Group as CourseSection).HideCaliperContext = true;
            envelope.Data.First().Membership.HideCaliperContext = true;
            envelope.Data.First().Session.HideCaliperContext = true;

            var coerced = JsonAssertions.coerce(envelope,
				new string[] { "..membership.member", "..membership.organization",
							"..generated.assignee", "..generated.assignable" });

			JsonAssertions.AssertSameObjectJson(coerced, "caliperEnvelopeEventSingle");
		}


		[Test]
		public void EnvelopeMixedBatch_MatchesReferenceJson() {

			var Assessment = new Assessment("https://example.edu/terms/201601/courses/7/sections/1/assess/1?ver=v1p0", defaultContextV1p1) {
				Name = "Quiz One",
				Items = new[] {
					new AssessmentItem("https://example.edu/terms/201601/courses/7/sections/1/assess/1/items/1", defaultContextV1p1) { HideCaliperContext = true },
					new AssessmentItem("https://example.edu/terms/201601/courses/7/sections/1/assess/1/items/2", defaultContextV1p1) { HideCaliperContext = true },
					new AssessmentItem("https://example.edu/terms/201601/courses/7/sections/1/assess/1/items/3", defaultContextV1p1) { HideCaliperContext = true }
                },
				DateCreated = CaliperTestEntities.Instant20160801060000,
				DateToStartOn = CaliperTestEntities.Instant20160816050000,
				DateToSubmit = CaliperTestEntities.Instant20160928115959,
				DateModified = CaliperTestEntities.Instant20160902113000,
				DatePublished = CaliperTestEntities.Instant20160815093000,
				DateToActivate = CaliperTestEntities.Instant20160816050000,
				DateToShow = CaliperTestEntities.Instant20160816050000,
				MaxAttempts = 2,
				MaxSubmits = 2,
				MaxScore = 15.0,
				Version = "1.0"
			};

			var CourseSection = new CourseSection
				("https://example.edu/terms/201601/courses/7/sections/1", defaultContextV1p1) {
				CourseNumber = "CPS 435-01",
				AcademicSession = "Fall 2016",
				Name = "CPS 435 Learning Analytics, Section 01",
				Category = "seminar",
				SubOrganizationOf = new CourseOffering("https://example.edu/terms/201601/courses/7", defaultContextV1p1) {
					CourseNumber = "CPS 435",
                    HideCaliperContext = true
                },
				DateCreated = CaliperTestEntities.Instant20160801060000
			};

			var AssessmentEventStarted = new AssessmentEvent(
				"urn:uuid:c51570e4-f8ed-4c18-bb3a-dfe51b2cc594", Action.Started, defaultContextV1p1) {
				Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
				Object = new Assessment("https://example.edu/terms/201601/courses/7/sections/1/assess/1?ver=v1p0", defaultContextV1p1),
				Generated = new Attempt("https://example.edu/terms/201601/courses/7/sections/1/assess/1/users/554433/attempts/1", defaultContextV1p1) {
					Assignee = CaliperTestEntities.Person554433(defaultContextV1p1),
					Assignable = new Assessment("https://example.edu/terms/201601/courses/7/sections/1/assess/1?ver=v1p0", defaultContextV1p1),
					Count = 1,
					DateCreated = CaliperTestEntities.Instant20161115101500,
					StartedAtTime = CaliperTestEntities.Instant20161115101500
				},
				EventTime = CaliperTestEntities.Instant20161115101500,
				EdApp = CaliperTestEntities.SoftwareAppV2(defaultContextV1p1),
				Group = CaliperTestEntities.CourseSectionCPS43501Fall16(defaultContextV1p1),
				Membership = CaliperTestEntities.EntityMembership554433Learner(defaultContextV1p1),
				Session = CaliperTestEntities.Session6259edu(defaultContextV1p1)
            };
            AssessmentEventStarted.Generated.HideCaliperContext = true;
            AssessmentEventStarted.Membership.HideCaliperContext = true;
            AssessmentEventStarted.Session.HideCaliperContext = true;

            var AssessmentEventSubmitted = new AssessmentEvent(
				"urn:uuid:dad88464-0c20-4a19-a1ba-ddf2f9c3ff33", Action.Submitted, defaultContextV1p1) {
				Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
				Object = new Assessment("https://example.edu/terms/201601/courses/7/sections/1/assess/1?ver=v1p0", defaultContextV1p1),
				Generated = new Attempt(
					"https://example.edu/terms/201601/courses/7/sections/1/assess/1/users/554433/attempts/1", defaultContextV1p1) {
					Assignee = CaliperTestEntities.Person554433(defaultContextV1p1),
					Assignable = new Assessment("https://example.edu/terms/201601/courses/7/sections/1/assess/1?ver=v1p0", defaultContextV1p1),
					Count = 1,
					DateCreated = CaliperTestEntities.Instant20161115101500,
					StartedAtTime = CaliperTestEntities.Instant20161115101500,
					EndedAtTime = CaliperTestEntities.Instant20161115105512,
					Duration = Period.FromMinutes(40) + Period.FromSeconds(12)
				},

				EventTime = CaliperTestEntities.Instant20161115102530,
				EdApp = CaliperTestEntities.SoftwareAppV2(defaultContextV1p1),
				Group = CaliperTestEntities.CourseSectionCPS43501Fall16(defaultContextV1p1),
				Membership = CaliperTestEntities.EntityMembership554433Learner(defaultContextV1p1),
				Session = CaliperTestEntities.Session6259edu(defaultContextV1p1)
            };
            AssessmentEventSubmitted.Generated.HideCaliperContext = true;
            AssessmentEventSubmitted.Membership.HideCaliperContext = true;
            AssessmentEventSubmitted.Session.HideCaliperContext = true;

            var GradeEvent = new GradeEvent(
						"urn:uuid:a50ca17f-5971-47bb-8fca-4e6e6879001d", Action.Graded, defaultContextV1p1) {
				Actor = CaliperTestEntities.AutoGraderV2(defaultContextV1p1),
				Object = new Attempt("https://example.edu/terms/201601/courses/7/sections/1/assess/1/users/554433/attempts/1", defaultContextV1p1) {
					Assignee = CaliperTestEntities.Person554433(defaultContextV1p1),
					Assignable = new Assessment("https://example.edu/terms/201601/courses/7/sections/1/assess/1?ver=v1p0", defaultContextV1p1),
					Count = 1,
					DateCreated = CaliperTestEntities.Instant20161115101500,
					StartedAtTime = CaliperTestEntities.Instant20161115101500,
					EndedAtTime = CaliperTestEntities.Instant20161115105512,
					Duration = Period.FromMinutes(40) + Period.FromSeconds(12)
				},
				Generated = CaliperTestEntities.Score1(defaultContextV1p1),
				EventTime = CaliperTestEntities.Instant20161115105706,
				EdApp = new SoftwareApplication("https://example.edu", defaultContextV1p1),
				Group = CaliperTestEntities.CourseSectionCPS43501Fall16(defaultContextV1p1)
            };
            (GradeEvent.Actor as SoftwareApplication).HideCaliperContext = true;
            GradeEvent.Object.HideCaliperContext = true;
            GradeEvent.Generated.HideCaliperContext = true;

            var envelope = new CaliperMessage<JObject>(defaultContextV1p1)
            {
				SensorId = CaliperTestEntities.EnvelopeDefaultSensorId,
				SendTime = CaliperTestEntities.EnvelopeDefaultSendTime,
				Data = new[] {
					clean(toJobject(CaliperTestEntities.Person554433dates(defaultContextV1p1))),
					clean(toJobject(Assessment)),
					clean(toJobject(CaliperTestEntities.SoftwareAppV2(defaultContextV1p1))),
					clean(toJobject(CourseSection)),
					clean(toJobject(AssessmentEventStarted)),
					clean(toJobject(AssessmentEventSubmitted)),
					clean(toJobject(GradeEvent))
				}
			};

			var coerced = JsonAssertions.coerce(envelope,
				new string[] { "..generated.assignable", "..generated.assignee",
				"..membership.member", "..membership.organization",
				"..edApp", "..group", "..object.assignable", "..object.assignee",
				"..generated.attempt", "..generated.scoredBy", "$.data[:6].actor",
					"$.data[:5].object" , "$.data[:6].object"});

			JsonAssertions.AssertSameObjectJson(coerced, "caliperEnvelopeMixedBatch", false);
		}

		[Test]
		public void EventAnnotationBookmarked_MatchesReferenceJson() {

			var page = new Page("https://example.com/#/texts/imscaliperimplguide/cfi/6/10!/4/2/2/2@0:0", defaultContextV1p1) {
				Name = "IMS Caliper Implementation Guide, pg 5",
				Version = "1.1",
                HideCaliperContext = true
			};

			var annotation = new BookmarkAnnotation(
				"https://example.com/users/554433/texts/imscaliperimplguide/bookmarks/1", defaultContextV1p1) {
				Annotator = CaliperTestEntities.Person554433(defaultContextV1p1),
				Annotated = page,
				BookmarkNotes = "Caliper profiles model discrete learning activities or supporting activities"
				+ " that facilitate learning.",
				DateCreated = CaliperTestEntities.Instant20161115101500
			};

			var bookmarkEvent = new AnnotationEvent("urn:uuid:d4618c23-d612-4709-8d9a-478d87808067", annotation, defaultContextV1p1) {
				Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
				Object = page,
				EventTime = CaliperTestEntities.Instant20161115101500,
				EdApp = CaliperTestEntities.EpubReader123(defaultContextV1p1),
				Group = CaliperTestEntities.CourseSectionCPS43501Fall16(defaultContextV1p1),
				Membership = CaliperTestEntities.EntityMembership554433Learner(defaultContextV1p1),
				Session = CaliperTestEntities.Session6259(defaultContextV1p1)
            };

            bookmarkEvent.Generated.HideCaliperContext = true;
            bookmarkEvent.EdApp.HideCaliperContext = true;
            (bookmarkEvent.Group as CourseSection).HideCaliperContext = true;
            bookmarkEvent.Membership.HideCaliperContext = true;
            bookmarkEvent.Session.HideCaliperContext = true;

            var coerced = JsonAssertions.coerce(bookmarkEvent,
				new string[] { "..generated.annotator", "..generated.annotated",
								"..membership.organization", "..membership.member" });

			JsonAssertions.AssertSameObjectJson(coerced, "caliperEventAnnotationBookmarked");
		}

		[Test]
		public void EventAnnotationHighlighted_MatchesReferenceJson() {

			var doc = new Document("https://example.com/#/texts/imscaliperimplguide", defaultContextV1p1) {
				Name = "IMS Caliper Implementation Guide",
				DateCreated = CaliperTestEntities.Instant20161001060000,
				Version = "1.1",
                HideCaliperContext = true
			};

			var annotation = new HighlightAnnotation(
				"https://example.com/users/554433/texts/imscaliperimplguide/highlights?start=2300&end=2370", defaultContextV1p1) {
				Annotator = CaliperTestEntities.Person554433(defaultContextV1p1),
				Annotated = doc,
				Selection = new TextPositionSelector {
					Start = 2300,
					End = 2370
				},
				SelectionText = "ISO 8601 formatted date and time expressed with millisecond precision.",
				DateCreated = CaliperTestEntities.Instant20161115101500,
                HideCaliperContext = true
            };

			var highlightEvent = new AnnotationEvent("urn:uuid:0067a052-9bb4-4b49-9d1a-87cd43da488a", annotation, defaultContextV1p1) {
				Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
				Object = doc,
				EventTime = CaliperTestEntities.Instant20161115101500,
				EdApp = CaliperTestEntities.EpubReader123(defaultContextV1p1),
				Group = CaliperTestEntities.CourseSectionCPS43501Fall16(defaultContextV1p1),
				Membership = CaliperTestEntities.EntityMembership554433Learner(defaultContextV1p1),
				Session = CaliperTestEntities.Session6259(defaultContextV1p1)
            };
            highlightEvent.EdApp.HideCaliperContext = true;
            (highlightEvent.Group as CourseSection).HideCaliperContext = true;
            highlightEvent.Membership.HideCaliperContext = true;
            highlightEvent.Session.HideCaliperContext = true;

            var coerced = JsonAssertions.coerce(highlightEvent,
				new string[] { "..generated.annotator", "..generated.annotated",
								"..membership.organization", "..membership.member" });

			JsonAssertions.AssertSameObjectJson(coerced, "caliperEventAnnotationHighlighted");
		}

		[Test]
		public void EventAnnotationShared_MatchesReferenceJson() {
			var doc = new Document("https://example.com/#/texts/imscaliperimplguide", defaultContextV1p1) {
				Name = "IMS Caliper Implementation Guide",
				Version = "1.1",
                HideCaliperContext = true
			};

			var annotation = new ShareAnnotation(
				"https://example.com/users/554433/texts/imscaliperimplguide/shares/1", defaultContextV1p1) {
				Annotator = CaliperTestEntities.Person554433(defaultContextV1p1),
				Annotated = doc,
				WithAgents = new[] {
					new Person("https://example.edu/users/657585", defaultContextV1p1) {
                        HideCaliperContext = true
                    },
					new Person("https://example.edu/users/667788", defaultContextV1p1) {
                        HideCaliperContext = true
                    }
				},
				DateCreated = CaliperTestEntities.Instant20161115101500,
                HideCaliperContext = true
            };

			var shareEvent = new AnnotationEvent("urn:uuid:3bdab9e6-11cd-4a0f-9d09-8e363994176b", annotation, defaultContextV1p1) {
				Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
				Object = doc,
				EventTime = CaliperTestEntities.Instant20161115101500,
				EdApp = CaliperTestEntities.EpubReader123(defaultContextV1p1),
				Group = CaliperTestEntities.CourseSectionCPS43501Fall16(defaultContextV1p1),
				Membership = CaliperTestEntities.EntityMembership554433Learner(defaultContextV1p1),
				Session = CaliperTestEntities.Session6259(defaultContextV1p1)
            };
            shareEvent.Object.HideCaliperContext = true;
            shareEvent.EdApp.HideCaliperContext = true;
            (shareEvent.Group as CourseSection).HideCaliperContext = true;
            shareEvent.Membership.HideCaliperContext = true;
            shareEvent.Session.HideCaliperContext = true;

            var coerced = JsonAssertions.coerce(shareEvent,
				new string[] { "..generated.annotator", "..generated.annotated",
								"..membership.organization", "..membership.member" });

			JsonAssertions.AssertSameObjectJson(coerced, "caliperEventAnnotationShared");
		}

		[Test]
		public void EventAnnotationTagged_MatchesReferenceJson() {
			var doc = new Page("https://example.com/#/texts/imscaliperimplguide/cfi/6/10!/4/2/2/2@0:0", defaultContextV1p1) {
				Name = "IMS Caliper Implementation Guide, pg 5",
				Version = "1.1",
                HideCaliperContext = true
            };

			var annotation = new TagAnnotation(
				"https://example.com/users/554433/texts/imscaliperimplguide/tags/3", defaultContextV1p1) {
				Annotator = CaliperTestEntities.Person554433(defaultContextV1p1),
				Annotated = doc,
				Tags = new[] { "profile", "event", "entity" },
				DateCreated = CaliperTestEntities.Instant20161115101500,
                HideCaliperContext = true
            };

			var tagEvent = new AnnotationEvent("urn:uuid:b2009c63-2659-4cd2-b71e-6e03c498f02b", annotation, defaultContextV1p1) {
				Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
				Object = doc,
				EventTime = CaliperTestEntities.Instant20161115101500,
				EdApp = CaliperTestEntities.EpubReader123(defaultContextV1p1),
				Group = CaliperTestEntities.CourseSectionCPS43501Fall16(defaultContextV1p1),
				Membership = CaliperTestEntities.EntityMembership554433Learner(defaultContextV1p1),
				Session = CaliperTestEntities.Session6259(defaultContextV1p1)
            };
            tagEvent.Object.HideCaliperContext = true;
            tagEvent.EdApp.HideCaliperContext = true;
            (tagEvent.Group as CourseSection).HideCaliperContext = true;
            tagEvent.Membership.HideCaliperContext = true;
            tagEvent.Session.HideCaliperContext = true;

            var coerced = JsonAssertions.coerce(tagEvent,
				new string[] { "..generated.annotator", "..generated.annotated",
								"..membership.organization", "..membership.member" });
			JsonAssertions.AssertSameObjectJson(coerced, "caliperEventAnnotationTagged");
		}

		[Test]
		public void EventAssessmentItemCompleted_MatchesReferenceJson() {

			var assessmentItemEvent = new AssessmentItemEvent(
				"urn:uuid:e5891791-3d27-4df1-a272-091806a43dfb", Action.Completed, defaultContextV1p1) {
				Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
				Object = new AssessmentItem("https://example.edu/terms/201601/courses/7/sections/1/assess/1/items/3", defaultContextV1p1) {
					Name = "Assessment Item 3",
					IsPartOf = new Assessment("https://example.edu/terms/201601/courses/7/sections/1/assess/1", defaultContextV1p1) { HideCaliperContext = true },
					DateToStartOn = CaliperTestEntities.Instant20161114050000,
					DateToSubmit = CaliperTestEntities.Instant20161118115959,
					MaxAttempts = 2,
					MaxSubmits = 2,
					MaxScore = 1.0,
					IsTimeDependent = false,
					Version = "1.0",
                    HideCaliperContext = true

				},
				Generated = new FillInBlankResponse(
					"https://example.edu/terms/201601/courses/7/sections/1/assess/1/items/3/users/554433/responses/1", defaultContextV1p1) {
					Attempt = CaliperTestEntities.Attempt1(defaultContextV1p1),
					DateCreated = CaliperTestEntities.Instant20161115101512,
					StartedAtTime = CaliperTestEntities.Instant20161115101502,
					EndedAtTime = CaliperTestEntities.Instant20161115101512,
					Values = new[] { "subject", "object", "predicate" },
                    HideCaliperContext = true
                },

				EventTime = CaliperTestEntities.Instant20161115101512,
				EdApp = CaliperTestEntities.SoftwareAppV2(defaultContextV1p1),
				Group = CaliperTestEntities.CourseSectionCPS43501Fall16(defaultContextV1p1),
				Membership = CaliperTestEntities.EntityMembership554433Learner(defaultContextV1p1),
				Session = CaliperTestEntities.Session6259edu(defaultContextV1p1)
            };
            assessmentItemEvent.Generated.Attempt.HideCaliperContext = true;
            assessmentItemEvent.Generated.Attempt.Assignable.HideCaliperContext = true;
            assessmentItemEvent.Generated.Attempt.Assignable.IsPartOf.HideCaliperContext = true;
            assessmentItemEvent.EdApp.HideCaliperContext = true;
            (assessmentItemEvent.Group as CourseSection).HideCaliperContext = true;
            assessmentItemEvent.Membership.HideCaliperContext = true;
            assessmentItemEvent.Session.HideCaliperContext = true;

            var coerced = JsonAssertions.coerce(assessmentItemEvent,
				new string[] { "..attempt.assignee", "..attempt.isPartOf", "..membership.member", "..membership.organization" });

			JsonAssertions.AssertSameObjectJson(coerced, "caliperEventAssessmentItemCompleted");
		}

		[Test]
		public void EventAssessmentItemSkipped_MatchesReferenceJson() {

			var assessmentItemEvent = new AssessmentItemEvent(
				"urn:uuid:04e27704-73bf-4d3c-912c-1b2da40aef8f", Action.Skipped, defaultContextV1p1) {
				Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
				Object = CaliperTestEntities.AssessmentItem2(defaultContextV1p1),
				EventTime = CaliperTestEntities.Instant20161115101430,
				EdApp = CaliperTestEntities.SoftwareAppV2(defaultContextV1p1),
				Group = CaliperTestEntities.CourseSectionCPS43501Fall16(defaultContextV1p1),
				Membership = CaliperTestEntities.EntityMembership554433Learner(defaultContextV1p1),
				Session = CaliperTestEntities.Session6259edu(defaultContextV1p1)
            };
            assessmentItemEvent.Object.HideCaliperContext = true;
            assessmentItemEvent.Object.IsPartOf.HideCaliperContext = true;
            assessmentItemEvent.EdApp.HideCaliperContext = true;
            (assessmentItemEvent.Group as CourseSection).HideCaliperContext = true;
            assessmentItemEvent.Membership.HideCaliperContext = true;
            assessmentItemEvent.Session.HideCaliperContext = true;

			var coerced = JsonAssertions.coerce(assessmentItemEvent,
				new string[] { "..membership.member", "..membership.organization" });

			JsonAssertions.AssertSameObjectJson(coerced, "caliperEventAssessmentItemSkipped");
		}

		[Test]
		public void EventAssessmentItemStarted_MatchesReferenceJson() {

			var assessmentItemEvent = new AssessmentItemEvent(
				"urn:uuid:1b557176-ba67-4624-b060-6bee670a3d8e", Action.Started, defaultContextV1p1) {
				Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
				Object = CaliperTestEntities.AssessmentItem3b(defaultContextV1p1),
				Generated = CaliperTestEntities.Attempt1b(defaultContextV1p1),
				EventTime = CaliperTestEntities.Instant20161115101500,
				EdApp = CaliperTestEntities.SoftwareAppV2(defaultContextV1p1),
				Group = CaliperTestEntities.CourseSectionCPS43501Fall16(defaultContextV1p1),
				Membership = CaliperTestEntities.EntityMembership554433Learner(defaultContextV1p1),
				Session = CaliperTestEntities.Session6259edu(defaultContextV1p1)
            };
            assessmentItemEvent.Object.HideCaliperContext = true;
            assessmentItemEvent.Object.IsPartOf.HideCaliperContext = true;
            assessmentItemEvent.Generated.HideCaliperContext = true;
            assessmentItemEvent.Generated.IsPartOf.HideCaliperContext = true;
            assessmentItemEvent.EdApp.HideCaliperContext = true;
            (assessmentItemEvent.Group as CourseSection).HideCaliperContext = true;
            assessmentItemEvent.Membership.HideCaliperContext = true;
            assessmentItemEvent.Session.HideCaliperContext = true;

            var coerced = JsonAssertions.coerce(assessmentItemEvent,
				new string[] { "..membership.member", "..membership.organization",
							"..generated.assignee", "..generated.assignable" });

			JsonAssertions.AssertSameObjectJson(coerced, "caliperEventAssessmentItemStarted");
		}

		[Test]
		public void EventAssessmentStarted_MatchesReferenceJson() {

			var assessmentEvent = new AssessmentEvent(
				"urn:uuid:27734504-068d-4596-861c-2315be33a2a2", Action.Started, defaultContextV1p1) {
				Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
				Object = CaliperTestEntities.AssessmentQuizOne(defaultContextV1p1),
				Generated = CaliperTestEntities.Attempt1c(defaultContextV1p1),
				EventTime = CaliperTestEntities.Instant20161115101500,
				EdApp = CaliperTestEntities.SoftwareAppV2(defaultContextV1p1),
				Group = CaliperTestEntities.CourseSectionCPS43501Fall16(defaultContextV1p1),
				Membership = CaliperTestEntities.EntityMembership554433Learner(defaultContextV1p1),
				Session = CaliperTestEntities.Session6259edu(defaultContextV1p1)
            };
            assessmentEvent.Object.HideCaliperContext = true;
            assessmentEvent.Generated.HideCaliperContext = true;
            assessmentEvent.EdApp.HideCaliperContext = true;
            (assessmentEvent.Group as CourseSection).HideCaliperContext = true;
            assessmentEvent.Membership.HideCaliperContext = true;
            assessmentEvent.Session.HideCaliperContext = true;

            var coerced = JsonAssertions.coerce(assessmentEvent,
				new string[] { "..membership.member", "..membership.organization",
							"..generated.assignee", "..generated.assignable" });

			JsonAssertions.AssertSameObjectJson(coerced, "caliperEventAssessmentStarted");
		}

		[Test]
		public void EventAssessmentSubmitted_MatchesReferenceJson() {

			var assessmentEvent = new AssessmentEvent(
				"urn:uuid:dad88464-0c20-4a19-a1ba-ddf2f9c3ff33", Action.Submitted, defaultContextV1p1) {
				Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
				Object = CaliperTestEntities.AssessmentQuizOne(defaultContextV1p1),
				Generated = CaliperTestEntities.Attempt2(defaultContextV1p1),
				EventTime = CaliperTestEntities.Instant20161115102530,
				EdApp = CaliperTestEntities.SoftwareAppV2(defaultContextV1p1),
				Group = CaliperTestEntities.CourseSectionCPS43501Fall16(defaultContextV1p1),
				Membership = CaliperTestEntities.EntityMembership554433Learner(defaultContextV1p1),
				Session = CaliperTestEntities.Session6259edu(defaultContextV1p1)
            };
            assessmentEvent.Object.HideCaliperContext = true;
            assessmentEvent.Generated.HideCaliperContext = true;
            assessmentEvent.EdApp.HideCaliperContext = true;
            (assessmentEvent.Group as CourseSection).HideCaliperContext = true;
            assessmentEvent.Membership.HideCaliperContext = true;
            assessmentEvent.Session.HideCaliperContext = true;

            var coerced = JsonAssertions.coerce(assessmentEvent,
				new string[] { "..generated.assignable", "..generated.assignee", "..membership.member", "..membership.organization",
							"..object.assignee" });

			JsonAssertions.AssertSameObjectJson(coerced, "caliperEventAssessmentSubmitted");
		}

		[Test]
		public void EventAssignableActivated_MatchesReferenceJson() {
			var assignableEvent = new AssignableEvent(
				"urn:uuid:2635b9dd-0061-4059-ac61-2718ab366f75", Action.Activated, defaultContextV1p1) {
				Actor = CaliperTestEntities.Person112233(defaultContextV1p1),
				Object = CaliperTestEntities.AssessmentQuizOneB(defaultContextV1p1),
				EventTime = CaliperTestEntities.Instant20161112101500,
				EdApp = CaliperTestEntities.SoftwareAppV2(defaultContextV1p1),
				Group = CaliperTestEntities.CourseSectionCPS43501Fall16(defaultContextV1p1),
				Membership = CaliperTestEntities.EntityMembership112233Instructor(defaultContextV1p1),
				Session = CaliperTestEntities.Session6259edu2(defaultContextV1p1)
            };
            (assignableEvent.Actor as Person).HideCaliperContext = true;
            assignableEvent.Object.HideCaliperContext = true;
            assignableEvent.EdApp.HideCaliperContext = true;
            (assignableEvent.Group as CourseSection).HideCaliperContext = true;
            assignableEvent.Membership.HideCaliperContext = true;
            assignableEvent.Session.HideCaliperContext = true;

			var coerced = JsonAssertions.coerce(assignableEvent,
				new string[] { "..membership.member", "..membership.organization" });

			JsonAssertions.AssertSameObjectJson(coerced, "caliperEventAssignableActivated");
		}

		[Test]
		public void EventBasicCreated_MatchesReferenceJson() {
			var evnt = new Event("urn:uuid:3a648e68-f00d-4c08-aa59-8738e1884f2c", defaultContextV1p1) {
				Action = Action.Created,
				Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
				Object = new Document("https://example.edu/terms/201601/courses/7/sections/1/resources/123", defaultContextV1p1) {
					Name = "Course Syllabus",
					DateCreated = CaliperTestEntities.Instant20161112071500,
					Version = "1",
                    HideCaliperContext = true
				},
				EventTime = CaliperTestEntities.Instant20161115101500
			};
			JsonAssertions.AssertSameObjectJson(evnt, "caliperEventBasicCreated");
		}

		[Test]
		public void EventBasicModifiedExtended_MatchesReferenceJson() {
			var evnt = new Event("urn:uuid:5973dcd9-3126-4dcc-8fd8-8153a155361c", defaultContextV1p1) {
				Action = Action.Modified,
				Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
				Object = new Document("https://example.edu/terms/201601/courses/7/sections/1/resources/123?version=3", defaultContextV1p1) {
					Name = "Course Syllabus",
					DateCreated = CaliperTestEntities.Instant20161112071500,
					DateModified = CaliperTestEntities.Instant20161115101500,
					Version = "3",
                    HideCaliperContext = true
                },
				EventTime = CaliperTestEntities.Instant20161115101500,
				Extensions = new ExtensionObject2()
			};

            foreach (Document archive in (evnt.Extensions as ExtensionObject2).Archive as IList)
            {
                archive.HideCaliperContext = true;
            }

			JsonAssertions.AssertSameObjectJson(evnt, "caliperEventBasicModifiedExtended");
		}

		class ExtensionObject2 {

			[JsonProperty("archive", Order = 90)]
			public IList Archive = new[] {
				new Document(
				"https://example.edu/terms/201601/courses/7/sections/1/resources/123?version=2", defaultContextV1p1) {
					DateCreated = CaliperTestEntities.Instant20161112071500,
					DateModified = CaliperTestEntities.Instant20161113110000,
					Version = "2"
				},
				new Document(
				"https://example.edu/terms/201601/courses/7/sections/1/resources/123?version=1", defaultContextV1p1) {
					DateCreated = CaliperTestEntities.Instant20161112071500,
					Version = "1"
				}
			};
		}

		[Test]
		public void EventForumSubscribed_MatchesReferenceJson() {
			var forumEvent = new ForumEvent(
				"urn:uuid:a2f41f9c-d57d-4400-b3fe-716b9026334e", Action.Subscribed, defaultContextV1p1) {
				Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
				Object = CaliperTestEntities.Forum1Caliper(defaultContextV1p1),
				EventTime = CaliperTestEntities.Instant20161115101600,
				EdApp = CaliperTestEntities.ForumAppV2(defaultContextV1p1),
				Group = CaliperTestEntities.CourseSectionCPS43501Fall16(defaultContextV1p1),
				Membership = CaliperTestEntities.EntityMembership554433Learner(defaultContextV1p1),
				Session = CaliperTestEntities.Session6259edu(defaultContextV1p1)
            };

            forumEvent.Object.HideCaliperContext = true;
            forumEvent.Object.IsPartOf.HideCaliperContext = true;
            forumEvent.EdApp.HideCaliperContext = true;
            (forumEvent.Group as CourseSection).HideCaliperContext = true;
            forumEvent.Membership.HideCaliperContext = true;
            forumEvent.Session.HideCaliperContext = true;

			var coerced = JsonAssertions.coerce(forumEvent,
				new string[] { "..membership.member", "..membership.organization" });

			JsonAssertions.AssertSameObjectJson(coerced, "caliperEventForumSubscribed");
		}

		[Test]
		public void EventMediaPausedVideo_MatchesReferenceJson() {

			var mediaEvent = new MediaEvent(
				"urn:uuid:956b4a02-8de0-4991-b8c5-b6eebb6b4cab", Action.Paused, defaultContextV1p1) {
				Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
				Object = CaliperTestEntities.VideoObject1(defaultContextV1p1),
				Target = new MediaLocation("https://example.edu/UQVK-dsU7-Y?t=321", defaultContextV1p1) {
					CurrentTime = Period.FromMinutes(5) + Period.FromSeconds(21),
                    HideCaliperContext = true
				},
				EventTime = CaliperTestEntities.Instant20161115101500,
				EdApp = new SoftwareApplication("https://example.edu/player", defaultContextV1p1),
				Group = CaliperTestEntities.CourseSectionCPS43501Fall16(defaultContextV1p1),
				Membership = CaliperTestEntities.EntityMembership554433Learner(defaultContextV1p1),
				Session = CaliperTestEntities.Session6259edu(defaultContextV1p1)
            };

            (mediaEvent.Object as VideoObject).HideCaliperContext = true;
            (mediaEvent.Group as CourseSection).HideCaliperContext = true;
            mediaEvent.Membership.HideCaliperContext = true;
            mediaEvent.Session.HideCaliperContext = true;

            JObject coerced = JsonAssertions.coerce(mediaEvent,
				new string[] { "..membership.member", "..membership.organization", "..edApp" });

			//nodatime period doesnt allow zero-padding, so fix in JObject state
			// (would otherwise be "PT5M21S")
			JToken tok = coerced.SelectToken("..currentTime");
			tok.Replace(JProperty.FromObject("PT05M21S"));

			JsonAssertions.AssertSameObjectJson(coerced, "caliperEventMediaPausedVideo");
		}

		[Test]
		public void EventMessagePosted_MatchesReferenceJson() {
			var messageEvent = new MessageEvent(
				"urn:uuid:0d015a85-abf5-49ee-abb1-46dbd57fe64e", Action.Posted, defaultContextV1p1) {
				Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
				Object = CaliperTestEntities.Message2(defaultContextV1p1),
				EventTime = CaliperTestEntities.Instant20161115101500,
				EdApp = CaliperTestEntities.ForumAppV2(defaultContextV1p1),
				Group = CaliperTestEntities.CourseSectionCPS43501Fall16(defaultContextV1p1),
				Membership = CaliperTestEntities.EntityMembership554433Learner(defaultContextV1p1),
				Session = CaliperTestEntities.Session6259edu(defaultContextV1p1)
            };

            messageEvent.Object.HideCaliperContext = true;
            messageEvent.Object.IsPartOf.HideCaliperContext = true;
            (messageEvent.Object.IsPartOf as Thread).IsPartOf.HideCaliperContext = true;
            messageEvent.EdApp.HideCaliperContext = true;
            (messageEvent.Group as CourseSection).HideCaliperContext = true;
            messageEvent.Membership.HideCaliperContext = true;
            messageEvent.Session.HideCaliperContext = true;

			var coerced = JsonAssertions.coerce(messageEvent,
				new string[] { "..membership.member", "..membership.organization" });

			JsonAssertions.AssertSameObjectJson(coerced, "caliperEventMessagePosted");
		}

		[Test]
		public void EventMessageReplied_MatchesReferenceJson() {
			var messageEvent = new MessageEvent(
				"urn:uuid:aed54386-a3fb-45ff-90f9-a35d3daaf031", Action.Posted, defaultContextV1p1) {
				Actor = CaliperTestEntities.Person778899(defaultContextV1p1),
				Object = CaliperTestEntities.Message3(defaultContextV1p1),
				EventTime = CaliperTestEntities.Instant20161115101530,
				EdApp = CaliperTestEntities.ForumAppV2(defaultContextV1p1),
				Group = CaliperTestEntities.CourseSectionCPS43501Fall16(defaultContextV1p1),
				Membership = CaliperTestEntities.EntityMembership778899Learner(defaultContextV1p1),
				Session = CaliperTestEntities.SessionCd50(defaultContextV1p1)
            };

            (messageEvent.Actor as Person).HideCaliperContext = true;
            messageEvent.Object.HideCaliperContext = true;
            messageEvent.Object.Creators.First().HideCaliperContext = true;
            messageEvent.Object.ReplyTo.HideCaliperContext = true;
            messageEvent.Object.IsPartOf.HideCaliperContext = true;
            (messageEvent.Object.IsPartOf as Thread).IsPartOf.HideCaliperContext = true;
            messageEvent.EdApp.HideCaliperContext = true;
            (messageEvent.Group as CourseSection).HideCaliperContext = true;
            messageEvent.Membership.HideCaliperContext = true;
            messageEvent.Session.HideCaliperContext = true;

            var coerced = JsonAssertions.coerce(messageEvent,
				new string[] { "..membership.member", "..membership.organization" });

			JsonAssertions.AssertSameObjectJson(coerced, "caliperEventMessageReplied");
		}

		[Test]
		public void EventNavigationNavigatedTo_MatchesReferenceJson() {
			var navEvent = new NavigationEvent(
				"urn:uuid:ff9ec22a-fc59-4ae1-ae8d-2c9463ee2f8f", defaultContextV1p1) {

				Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
				Object = CaliperTestEntities.WebPage2(defaultContextV1p1),
				EventTime = CaliperTestEntities.Instant20161115101500,
				Referrer = new WebPage("https://example.edu/terms/201601/courses/7/sections/1/pages/1", defaultContextV1p1),
				EdApp = new SoftwareApplication("https://example.edu", defaultContextV1p1),
				Group = CaliperTestEntities.CourseSectionCPS43501Fall16(defaultContextV1p1),
				Membership = CaliperTestEntities.EntityMembership554433Learner(defaultContextV1p1),
				Session = CaliperTestEntities.Session6259edu(defaultContextV1p1)
            };

            (navEvent.Object as WebPage).HideCaliperContext = true;
            (navEvent.Group as CourseSection).HideCaliperContext = true;
            navEvent.Membership.HideCaliperContext = true;
            navEvent.Session.HideCaliperContext = true;
            navEvent.Referrer.HideCaliperContext = true;

			var coerced = JsonAssertions.coerce(navEvent,
				new string[] { "..membership.member", "..membership.organization", "..edApp" });

			JsonAssertions.AssertSameObjectJson(coerced, "caliperEventNavigationNavigatedTo");
		}

		[Test]
		public void EventViewViewedFedSession_MatchesReferenceJson() {
			var viewEvent = new ViewEvent(
				"urn:uuid:4be6d29d-5728-44cd-8a8f-3d3f07e46b61", defaultContextV1p1) {

				Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
				Object = CaliperTestEntities.Epub202(defaultContextV1p1),
				EventTime = CaliperTestEntities.Instant20161115102000,
				EdApp = new SoftwareApplication("https://example.com", defaultContextV1p1),
				Group = CaliperTestEntities.CourseSectionCPS43501Fall16b(defaultContextV1p1),
				Membership = CaliperTestEntities.EntityMembership554433Learner(defaultContextV1p1),
				Session = CaliperTestEntities.Session1241(defaultContextV1p1),
				FederatedSession = new LtiSession(
                    "https://example.edu/lti/sessions/b533eb02823f31024e6b7f53436c42fb99b31241", defaultContextV1p1) {
					User = CaliperTestEntities.Person554433(defaultContextV1p1),
                    MessageParameters = new CaliperTestEntities.LtiParamsLtiSession(),
                    DateCreated = CaliperTestEntities.Instant20181115101500,
					StartedAt = CaliperTestEntities.Instant20181115101500
                }
			};

            (viewEvent.Object as Document).HideCaliperContext = true;
            (viewEvent.Group as CourseSection).HideCaliperContext = true;
            viewEvent.Membership.HideCaliperContext = true;
            viewEvent.FederatedSession.HideCaliperContext = true;
            viewEvent.Session.HideCaliperContext = true;

            var coerced = JsonAssertions.coerce(viewEvent,
				new string[] { "..membership.member", "..membership.organization", "..edApp" });

			JsonAssertions.AssertSameObjectJson(coerced, "caliperEventViewViewedFedSession");
		}

		[Test]
		public void EventNavigationNavigatedToThinned_MatchesReferenceJson() {
			var navEvent = new NavigationEvent(
				"urn:uuid:71657137-8e6e-44f8-8499-e1c3df6810d2", defaultContextV1p1) {

				Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
				Object = CaliperTestEntities.WebPage2(defaultContextV1p1),
				EventTime = CaliperTestEntities.Instant20161115101500,
				Referrer = new WebPage("https://example.edu/terms/201601/courses/7/sections/1/pages/1", defaultContextV1p1),
				EdApp = new SoftwareApplication("https://example.edu", defaultContextV1p1),
				Group = CaliperTestEntities.CourseSectionCPS43501Fall16(defaultContextV1p1),
				Membership = CaliperTestEntities.EntityMembership554433Learner(defaultContextV1p1),
				Session = CaliperTestEntities.Session6259edu(defaultContextV1p1)
            };

			var coerced = JsonAssertions.coerce(navEvent,
				new string[] { "..actor", "..object", "..referrer", "..edApp",
				"..group", "..membership", "..session" });

			JsonAssertions.AssertSameObjectJson(coerced, "caliperEventNavigationNavigatedToThinned");
		}


		[Test]
		public void EventGradeGraded_MatchesReferenceJson() {
			var gradeEvent = new GradeEvent("urn:uuid:a50ca17f-5971-47bb-8fca-4e6e6879001d", Action.Graded, defaultContextV1p1)
            {
                Actor = CaliperTestEntities.AutoGraderV2(defaultContextV1p1),
                Object = CaliperTestEntities.Attempt1d(defaultContextV1p1),
                Generated = CaliperTestEntities.Score1(defaultContextV1p1),
                EventTime = CaliperTestEntities.Instant20161115105706,
                EdApp = new SoftwareApplication("https://example.edu", defaultContextV1p1),
                Group = CaliperTestEntities.CourseSectionCPS43501Fall16(defaultContextV1p1)
            };

            (gradeEvent.Actor as SoftwareApplication).HideCaliperContext = true;
            (gradeEvent.Object as Attempt).HideCaliperContext = true;
            (gradeEvent.Object as Attempt).Assignable.HideCaliperContext = true;
            (gradeEvent.Generated as Score).HideCaliperContext = true;
            (gradeEvent.Group as CourseSection).HideCaliperContext = true;

            var coerced = JsonAssertions.coerce(gradeEvent,
				new string[] { "..edApp", "..scoredBy", "..generated.attempt" });

			JsonAssertions.AssertSameObjectJson(coerced, "caliperEventGradeGraded");
		}


		[Test]
		public void EventGradeGradedItem_MatchesReferenceJson() {
			var gradeEvent = new GradeEvent(
				"urn:uuid:12c05c4e-253f-4073-9f29-5786f3ff3f36", Action.Graded, defaultContextV1p1) {

				Actor = CaliperTestEntities.AutoGraderV2(defaultContextV1p1),
				Object = CaliperTestEntities.Attempt1(defaultContextV1p1),
				EventTime = CaliperTestEntities.Instant20161115105706,
				EdApp = new SoftwareApplication("https://example.edu", defaultContextV1p1),
				Generated = CaliperTestEntities.Score1b(defaultContextV1p1),
				Group = CaliperTestEntities.CourseSectionCPS43501Fall16(defaultContextV1p1)
            };

            (gradeEvent.Actor as SoftwareApplication).HideCaliperContext = true;
            (gradeEvent.Object as Attempt).HideCaliperContext = true;
            (gradeEvent.Object as Attempt).Assignable.HideCaliperContext = true;
            (gradeEvent.Object as Attempt).Assignable.IsPartOf.HideCaliperContext = true;
            gradeEvent.Generated.HideCaliperContext = true;
            (gradeEvent.Group as CourseSection).HideCaliperContext = true;

            var coerced = JsonAssertions.coerce(gradeEvent,
				new string[] { "..edApp", "..scoredBy", "..generated.attempt",
							"..object.isPartOf" });
			JsonAssertions.AssertSameObjectJson(coerced, "caliperEventGradeGradedItem");
		}

		[Test]
		public void EventSessionLoggedIn_MatchesReferenceJson() {
			var sessionEvent = new SessionEvent(
				"urn:uuid:fcd495d0-3740-4298-9bec-1154571dc211", Action.LoggedIn, defaultContextV1p1) {

				Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
				Object = CaliperTestEntities.SoftwareAppV2(defaultContextV1p1),
				EventTime = CaliperTestEntities.Instant20161115101500,
				EdApp = CaliperTestEntities.SoftwareAppV2(defaultContextV1p1),
				Session = CaliperTestEntities.Session6259b(defaultContextV1p1)
            };

            sessionEvent.Object.HideCaliperContext = true;
            sessionEvent.Session.HideCaliperContext = true;

            var coerced = JsonAssertions.coerce(sessionEvent,
				new string[] { "..edApp", "..session.user" });

			JsonAssertions.AssertSameObjectJson(coerced, "caliperEventSessionLoggedIn");
		}

		[Test]
		public void EventSessionLoggedInExtended_MatchesReferenceJson() {

			var sessionEvent = new SessionEvent(
				"urn:uuid:4ec2c31e-3ec0-4fe1-a017-b81561b075d7", Action.LoggedIn, defaultContextV1p1) {

				Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
				Object = CaliperTestEntities.SoftwareAppV2(defaultContextV1p1),
				EventTime = CaliperTestEntities.Instant20161115201115,
				EdApp = CaliperTestEntities.SoftwareAppV2(defaultContextV1p1),
				Session = CaliperTestEntities.Session6259c(defaultContextV1p1)
            };

            sessionEvent.Object.HideCaliperContext = true;
            sessionEvent.Session.HideCaliperContext = true;

			sessionEvent.Session.Extensions = new RequestExtension();

			var coerced = JsonAssertions.coerce(sessionEvent,
				new string[] { "..edApp", "..session.user" });

			JsonAssertions.AssertSameObjectJson(coerced, "caliperEventSessionLoggedInExtended");
		}

		class RequestExtension {
			[JsonProperty("request")]
			public object request = new {
				id = "d71016dc-ed2f-46f9-ac2c-b93f15f38fdc",
				hostname = "example.com",
				userAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36",
			};
		}

		[Test]
		public void EventSessionLoggedOut_MatchesReferenceJson() {
			var sessionEvent = new SessionEvent(
				"urn:uuid:a438f8ac-1da3-4d48-8c86-94a1b387e0f6", Action.LoggedOut, defaultContextV1p1) {

				Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
				Object = CaliperTestEntities.SoftwareAppV2(defaultContextV1p1),
				EventTime = CaliperTestEntities.Instant20161115110500,
				EdApp = CaliperTestEntities.SoftwareAppV2(defaultContextV1p1),
				Session = CaliperTestEntities.Session6259d(defaultContextV1p1)
            };

            sessionEvent.Object.HideCaliperContext = true;
            sessionEvent.Session.HideCaliperContext = true;

            var coerced = JsonAssertions.coerce(sessionEvent,
				new string[] { "..edApp", "..session.user" });

			JsonAssertions.AssertSameObjectJson(coerced, "caliperEventSessionLoggedOut");
		}

		[Test]
		public void EventSessionTimedOut_MatchesReferenceJson() {
			var sessionEvent = new SessionEvent(
				"urn:uuid:4e61cf6c-ffbe-45bc-893f-afe7ad4079dc", Action.TimedOut, defaultContextV1p1) {

				Actor = new SoftwareApplication("https://example.edu", defaultContextV1p1),
				Object = new Session(
					"https://example.edu/sessions/7d6b88adf746f0692e2e873308b78c60fb13a864", defaultContextV1p1) {
					User = CaliperTestEntities.Person112233(defaultContextV1p1),
					DateCreated = CaliperTestEntities.Instant20161115101500,
					StartedAt = CaliperTestEntities.Instant20161115101500,
					EndedAt = CaliperTestEntities.Instant20161115111500,
					Duration = Period.FromSeconds(3600)

				},
				EventTime = CaliperTestEntities.Instant20161115111500,
				EdApp = CaliperTestEntities.SoftwareAppV2(defaultContextV1p1)
			};

            (sessionEvent.Actor as SoftwareApplication).HideCaliperContext = true;
            sessionEvent.Object.HideCaliperContext = true;
            (sessionEvent.Object as Session).User.HideCaliperContext = true;

			var coerced = JsonAssertions.coerce(sessionEvent,
				new string[] { "..edApp" });

			JsonAssertions.AssertSameObjectJson(coerced, "caliperEventSessionTimedOut");
		}

		[Test]
		public void EventThreadMarkedAsRead_MatchesReferenceJson() {
			var threadEvent = new ThreadEvent(
				"urn:uuid:6b20c5ba-301c-4e56-85a0-2f3d9a94c249", Action.MarkedAsRead, defaultContextV1p1) {

				Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
				Object = CaliperTestEntities.Thread1(defaultContextV1p1),
				EventTime = CaliperTestEntities.Instant20161115101600,
				EdApp = CaliperTestEntities.ForumAppV2(defaultContextV1p1),
				Group = CaliperTestEntities.CourseSectionCPS43501Fall16(defaultContextV1p1),
				Membership = CaliperTestEntities.EntityMembership554433Learner(defaultContextV1p1),
				Session = CaliperTestEntities.Session6259edu(defaultContextV1p1)
            };

            threadEvent.Object.HideCaliperContext = true;
            threadEvent.Object.IsPartOf.HideCaliperContext = true;
            threadEvent.EdApp.HideCaliperContext = true;
            (threadEvent.Group as CourseSection).HideCaliperContext = true;
            threadEvent.Membership.HideCaliperContext = true;
            threadEvent.Session.HideCaliperContext = true;

			var coerced = JsonAssertions.coerce(threadEvent,
				new string[] { "..membership.member", "..membership.organization" });
			JsonAssertions.AssertSameObjectJson(coerced, "caliperEventThreadMarkedAsRead");
		}

		[Test]
		public void EventToolUseUsed_MatchesReferenceJson() {
			var toolUseEvent = new ToolUseEvent(
				"urn:uuid:7e10e4f3-a0d8-4430-95bd-783ffae4d916", Action.Used, defaultContextV1p1) {

				Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
				Object = new SoftwareApplication("https://example.edu", defaultContextV1p1),
				EventTime = CaliperTestEntities.Instant20161115101500,
				EdApp = CaliperTestEntities.SoftwareAppV2(defaultContextV1p1),
				Group = CaliperTestEntities.CourseSectionCPS43501Fall16(defaultContextV1p1),
				Membership = CaliperTestEntities.EntityMembership554433Learner(defaultContextV1p1),
				Session = CaliperTestEntities.Session6259edu(defaultContextV1p1)
            };

            (toolUseEvent.Object as SoftwareApplication).HideCaliperContext = true;
            (toolUseEvent.Group as CourseSection).HideCaliperContext = true;
            toolUseEvent.Membership.HideCaliperContext = true;
            toolUseEvent.Session.HideCaliperContext = true;

			var coerced = JsonAssertions.coerce(toolUseEvent,
				new string[] { "..membership.member", "..membership.organization", "..edApp" });

			JsonAssertions.AssertSameObjectJson(coerced, "caliperEventToolUseUsed");
        }

        [Test]
        public void EventToolUseUsedWithProgress_MatchesReferenceJson()
        {
            var person = new Person("https://example.edu/users/554433", defaultContextV1p1) { HideCaliperContext = true };

            var toolUseEvent = new ToolUseEvent("urn:uuid:7e10e4f3-a0d8-4430-95bd-783ffae4d916", Action.Used, toolUseProfileExtensionV1p1)
            {
                Context = toolUseProfileExtensionV1p1,
                Actor = person,
                Action = Action.Used,
                Object = new SoftwareApplication("https://example.edu", defaultContextV1p1) { HideCaliperContext = true},
                EventTime = Instant.FromUtc(2019, 11, 15, 10, 15, 00),
                EdApp = CaliperTestEntities.SoftwareAppV2(defaultContextV1p1),
                Generated = CaliperTestEntities.AggregateMeasureCollection2019(defaultContextV1p1),
                Group = new CourseSection("https://example.edu/terms/201601/courses/7/sections/1", defaultContextV1p1)
                {
                    CourseNumber = "CPS 435-01",
                    Name = "CPS 435 Learning Analytics, Section 01",
                    AcademicSession = "Fall 2016",
                    Category = "seminar",
                    SubOrganizationOf = new CourseOffering("https://example.edu/terms/201601/courses/7", defaultContextV1p1)
                    {
                        CourseNumber = "CPS 435",
                        HideCaliperContext = true
                    },
                    DateCreated = CaliperTestEntities.Instant20160801060000,
                    HideCaliperContext = true
                },
                Membership = new Membership("https://example.edu/terms/201601/courses/7/sections/1/rosters/1/members/554433", defaultContextV1p1)
                {
                    Member = person,
                    Organization = new CourseSection("https://example.edu/terms/201601/courses/7/sections/1", defaultContextV1p1)
                    {
                        SubOrganizationOf = new CourseOffering("https://example.edu/terms/201601/courses/7", defaultContextV1p1) { HideCaliperContext = true },
                        HideCaliperContext = true
                    },
                    Roles = new[] { Role.Learner },
                    Status = Status.Active,
                    DateCreated = CaliperTestEntities.Instant20161101060000,
                    HideCaliperContext = true
                },
                Session = new Session("https://example.edu/sessions/1f6442a482de72ea6ad134943812bff564a76259", defaultContextV1p1)
                {
                    User = person,
                    StartedAt = Instant.FromUtc(2016, 09, 15, 10, 00, 00),
                    HideCaliperContext = true
                },
            };

            var coerced = JsonAssertions.coerce(toolUseEvent, new string[] { "..edApp" });

            JsonAssertions.AssertSameObjectJson(coerced, "caliperEventToolUseUsedWithProgress");
        }

        [Test]
		public void EventViewViewed_MatchesReferenceJson() {
			var viewEvent = new ViewEvent(
				"urn:uuid:cd088ca7-c044-405c-bb41-0b2a8506f907", defaultContextV1p1) {
				Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
				Object = CaliperTestEntities.Epub201(defaultContextV1p1),
				EventTime = CaliperTestEntities.Instant20161115101500,
				EdApp = CaliperTestEntities.SoftwareAppV2(defaultContextV1p1),
				Group = CaliperTestEntities.CourseSectionCPS43501Fall16(defaultContextV1p1),
				Membership = CaliperTestEntities.EntityMembership554433Learner(defaultContextV1p1),
				Session = CaliperTestEntities.Session6259edu(defaultContextV1p1)
            };

            (viewEvent.Object as Document).HideCaliperContext = true;
            (viewEvent.Group as CourseSection).HideCaliperContext = true;
            viewEvent.Membership.HideCaliperContext = true;
            viewEvent.Session.HideCaliperContext = true;

            var coerced = JsonAssertions.coerce(viewEvent,
				new string[] { "..membership.member", "..membership.organization", "..edApp" });
			JsonAssertions.AssertSameObjectJson(coerced, "caliperEventViewViewed");
		}

		[Test]
		public void EventViewViewedExtended_MatchesReferenceJson() {
			var viewEvent = new ViewEvent(
				"urn:uuid:3a9bd869-addc-48b1-80f6-a14b2ff591ed", defaultContextV1p1) {
				Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
				Object = CaliperTestEntities.Epub200(defaultContextV1p1),
				EventTime = CaliperTestEntities.Instant20161115101500,
				EdApp = CaliperTestEntities.SoftwareAppV2(defaultContextV1p1),
				Group = CaliperTestEntities.CourseSectionCPS43501Fall16(defaultContextV1p1),
				Membership = CaliperTestEntities.EntityMembership554433Learner(defaultContextV1p1),
				Session = CaliperTestEntities.Session6259edu(defaultContextV1p1),
				Extensions = new ViewEventExtension1()
			};

            (viewEvent.Object as Document).HideCaliperContext = true;
            (viewEvent.Group as CourseSection).HideCaliperContext = true;
            viewEvent.Membership.HideCaliperContext = true;
            viewEvent.Session.HideCaliperContext = true;

			var coerced = JsonAssertions.coerce(viewEvent,
				new string[] { "..membership.member", "..membership.organization", "..edApp" });

			JsonAssertions.AssertSameObjectJson(coerced, "caliperEventViewViewedExtended");
		}

		public class ViewEventExtension1 {
			[JsonProperty("job")]
			public object Job = new {
				id = "08c1233d-9ba3-40ac-952f-004c47a50ff7",
				jobTag = "caliper_batch_job",
				jobDate = "2016-11-16T01:01:00.000Z",
			};
		}

        [Test]
        public void EventSearchSearched_MatchesReferenceJson()
        {
            var searchEvent = new SearchEvent(
                "urn:uuid:cb3878ed-8240-4c6d-9fee-77221810f5e4", Action.Searched, searchProfileExtensionV1p1)
            {
                Actor = new Person("https://example.edu/users/554433", defaultContextV1p1) { HideCaliperContext = true },
                Object = CaliperTestEntities.CatalogApp(defaultContextV1p1),
                EventTime = CaliperTestEntities.Instant20181115100500,
                Generated = CaliperTestEntities.SearchIMSCaliperAnalytics(defaultContextV1p1),
                EdApp = CaliperTestEntities.SoftwareAppV2(defaultContextV1p1),
                Group = CaliperTestEntities.CourseSectionCPS43501Fall18(defaultContextV1p1),
                Membership = CaliperTestEntities.EntityMembership554433Learner_2018(defaultContextV1p1),
                Session = CaliperTestEntities.Session6259_2018(defaultContextV1p1)
            };

            var coerced = JsonAssertions.coerce(searchEvent,
                new string[] { "..edApp", "..searchProvider", "..searchTarget", "..query.creator", "..query.searchTarget", "..generated.searchResults", "..membership.member", "..membership.organization" });

            JsonAssertions.AssertSameObjectJson(coerced, "caliperEventSearchSearched");
        }

        [Test]
        public void EventToolLaunchLaunched_MatchesReferenceJson()
        {
            var toolLaunchEvent = new ToolLaunchEvent(
                "urn:uuid:a2e8b214-4d4a-4456-bb4c-099945749117", Action.Launched, toolLaunchProfileExtensionV1p1)
            {
                Actor = new Person("https://example.edu/users/554433", defaultContextV1p1) { HideCaliperContext = true },
                Object = new SoftwareApplication("https://example.com/lti/tool", defaultContextV1p1) { HideCaliperContext = true },
                EventTime = CaliperTestEntities.Instant20181115101500,
                EdApp = new SoftwareApplication("https://example.edu", defaultContextV1p1) { HideCaliperContext = true },
                Referrer = new Entity("https://example.edu/terms/201801/courses/7/sections/1/pages/1", defaultContextV1p1) { Type = EntityType.WebPage, HideCaliperContext = true },
                Group = new CourseSection("https://example.edu/terms/201801/courses/7/sections/1", defaultContextV1p1)
                {
                    CourseNumber = "CPS 435-01",
                    AcademicSession = "Fall 2018",
                    HideCaliperContext = true
                },
                Membership = new Membership("https://example.edu/terms/201801/courses/7/sections/1/rosters/1", defaultContextV1p1)
                {
                    Member = new Person("https://example.edu/users/554433", defaultContextV1p1),
                    Organization = new Organization("https://example.edu/terms/201801/courses/7/sections/1", defaultContextV1p1),
                    Roles = new[] { Role.Learner },
                    Status = Status.Active,
                    DateCreated = CaliperTestEntities.Instant20180801060000,
                    HideCaliperContext = true
                },
                Session = CaliperTestEntities.Session6259_2018(defaultContextV1p1),
                Target = new LtiLink("https://tool.com/link/123", defaultContextV1p1)
                {
                    MessageType = EntityType.LtiResourceLinkRequest,
                    HideCaliperContext = true
                },
                FederatedSession = new LtiSession("https://example.edu/lti/sessions/b533eb02823f31024e6b7f53436c42fb99b31241", defaultContextV1p1)
                {
                    User = new Person("https://example.edu/users/554433", defaultContextV1p1) { HideCaliperContext = true },
                    MessageParameters = new CaliperTestEntities.LtiParamsLtiSession(),
                    DateCreated = CaliperTestEntities.Instant20181115101500,
                    StartedAt = CaliperTestEntities.Instant20181115101500,
                    HideCaliperContext = true
                }
            };

            var coerced = JsonAssertions.coerce(toolLaunchEvent, new[] { "..membership.member", "..membership.organization" });

            JsonAssertions.AssertSameObjectJson(coerced, "caliperEventToolLaunchLaunched");
        }

        [Test]
        public void EventToolLaunchReturned_MatchesReferenceJson()
        {
            var toolLaunchEvent = new ToolLaunchEvent(
                "urn:uuid:a2e8b214-4d4a-4456-bb4c-099945749117", Action.Returned, toolLaunchProfileExtensionV1p1)
            {
                Actor = new Person("https://example.edu/users/554433", defaultContextV1p1) { HideCaliperContext = true },
                Object = new SoftwareApplication("https://example.com/lti/tool", defaultContextV1p1) { HideCaliperContext = true },
                EventTime = CaliperTestEntities.Instant20181115101500,
                EdApp = new SoftwareApplication("https://example.edu", defaultContextV1p1) { HideCaliperContext = true },
                Referrer = new Entity("https://tool.com/lti/123") { Type = EntityType.LtiLink, HideCaliperContext = true },
                Group = new CourseSection("https://example.edu/terms/201801/courses/7/sections/1", defaultContextV1p1)
                {
                    CourseNumber = "CPS 435-01",
                    AcademicSession = "Fall 2018",
                    HideCaliperContext = true
                },
                Membership = new Membership("https://example.edu/terms/201801/courses/7/sections/1/rosters/1", defaultContextV1p1)
                {
                    Member = new Person("https://example.edu/users/554433", defaultContextV1p1),
                    Organization = new Organization("https://example.edu/terms/201801/courses/7/sections/1", defaultContextV1p1),
                    Roles = new[] { Role.Learner },
                    Status = Status.Active,
                    DateCreated = CaliperTestEntities.Instant20180801060000,
                    HideCaliperContext = true
                },
                Session = CaliperTestEntities.Session6259_2018(defaultContextV1p1),
                Target = new Link("https://example.edu/terms/201801/courses/7/sections/1/pages/1", defaultContextV1p1) { HideCaliperContext = true },
                FederatedSession = new LtiSession("https://example.edu/lti/sessions/b533eb02823f31024e6b7f53436c42fb99b31241", defaultContextV1p1)
                {
                    User = new Person("https://example.edu/users/554433", defaultContextV1p1) { HideCaliperContext = true },
                    DateCreated = CaliperTestEntities.Instant20181115101500,
                    StartedAt = CaliperTestEntities.Instant20181115101500,
                    HideCaliperContext = true
                }
            };

            var coerced = JsonAssertions.coerce(toolLaunchEvent, new[] { "..membership.member", "..membership.organization" });

            JsonAssertions.AssertSameObjectJson(coerced, "caliperEventToolLaunchReturned");
        }

        [Test]
        public void EntityAggregateMeasure_MatchesReferenceJson()
        {
            var aggregateMeasure = new AggregateMeasure("urn:uuid:c3ba4c01-1f17-46e0-85dd-1e366e6ebb81", toolUseProfileExtensionV1p1)
            {
                Metric = MetricUnitType.UnitsCompleted,
                Name = "Units Completed",
                MetricValue = 12.0,
                MaxMetricValue = 25.0,
                StartedAtTime = Instant.FromUtc(2019, 08, 15, 10, 15, 00),
                EndedAtTime = Instant.FromUtc(2019, 11, 15, 10, 15, 00)
            };

            JsonAssertions.AssertSameObjectJson(aggregateMeasure, "caliperEntityAggregateMeasure");
        }

        [Test]
        public void EntityAggregateMeasureCollection_MatchesReferenceJson()
        {
            var aggregateMeasureCollection = new AggregateMeasureCollection("urn:uuid:60b4db01-f1e5-4a7f-add9-6a8f761625b1", toolUseProfileExtensionV1p1)
            {
                Items = new[] {
                    new AggregateMeasure("urn:uuid:21c3f9f2-a9ef-4f65-bf9a-0699ed85e2c7", defaultContextV1p1)
                    {
                        Metric = MetricUnitType.MinutesOnTask,
                        Name = "Minutes On Task",
                        MetricValue = 873.0,
                        StartedAtTime = Instant.FromUtc(2019, 08, 15, 10, 15, 00),
                        EndedAtTime = Instant.FromUtc(2019, 11, 15, 10, 15, 00),
                        HideCaliperContext = true
                    },
                    new AggregateMeasure("urn:uuid:c3ba4c01-1f17-46e0-85dd-1e366e6ebb81", defaultContextV1p1)
                    {
                        Metric = MetricUnitType.UnitsCompleted,
                        Name = "Units Completed",
                        MetricValue = 12.0,
                        MaxMetricValue = 25.0,
                        StartedAtTime = Instant.FromUtc(2019, 08, 15, 10, 15, 00),
                        EndedAtTime = Instant.FromUtc(2019, 11, 15, 10, 15, 00),
                        HideCaliperContext = true
                    }
                }
            };

            JsonAssertions.AssertSameObjectJson(aggregateMeasureCollection, "caliperEntityAggregateMeasureCollection");
        }

        [Test]
        public void EntityLtiLink_MatchesReferenceJson()
        {
            var ltiLink = new LtiLink("https://tool.com/link/123", toolLaunchProfileExtensionV1p1)
            {
                MessageType = EntityType.LtiResourceLinkRequest
            };

            JsonAssertions.AssertSameObjectJson(ltiLink, "caliperEntityLtiLink");
        }

        [Test]
        public void EntityQuery_MatchesReferenceJson()
        {
            var ltiLink = new Query("https://example.edu/users/554433/search?query=IMS%20AND%20%28Caliper%20OR%20Analytics%29", searchProfileExtensionV1p1)
            {
                Creator = new Person("https://example.edu/users/554433", defaultContextV1p1) { HideCaliperContext = true },
                SearchTarget = new SoftwareApplication("https://example.edu/catalog", defaultContextV1p1) { HideCaliperContext = true },
                SearchTerms = "IMS AND (Caliper OR Analytics)",
                DateCreated = CaliperTestEntities.Instant20181115100500
            };

            JsonAssertions.AssertSameObjectJson(ltiLink, "caliperEntityQuery");
        }

        [Test]
        public void EntityComment_MatchesReferenceJson()
        {
            var entity = new FeedbackComment("https://example.edu/terms/201801/courses/7/sections/1/assess/1/items/6/users/665544/responses/1/comment/1")
            {
                Commenter = new Person("https://example.edu/users/554433") { HideCaliperContext = true },
                CommentedOn = new DigitalResource("https://example.edu/terms/201801/courses/7/sections/1/resources/1/syllabus.pdf")
                {
                    Name = "Course Syllabus",
                    MediaType = "application/pdf",
                    IsPartOf = new DigitalResourceCollection("https://example.edu/terms/201801/courses/7/sections/1/resources/1")
                    {
                        Name = "Course Assets",
                        IsPartOf = new CourseSection("https://example.edu/terms/201801/courses/7/sections/1") { HideCaliperContext = true },
                        HideCaliperContext = true
                    },
                    DateCreated = Instant.FromUtc(2018, 08, 02, 11, 32, 00),
                    HideCaliperContext = true
                },
                Value = "I like what you did here but you need to improve on...",
                DateCreated = CaliperTestEntities.Instant20180801060000
            };
            
            JsonAssertions.AssertSameObjectJson(entity, "caliperEntityComment");
        }

        [Test]
        public void EntityScale_MatchesReferenceJson()
        {
            var entity = new Scale("https://example.edu/scale/1")
            {
                DateCreated = CaliperTestEntities.Instant20180801060000
            };

            JsonAssertions.AssertSameObjectJson(entity, "caliperEntityScale");
        }

        [Test]
        public void EntityLikertScale_MatchesReferenceJson()
        {
            var entity = new LikertScale("https://example.edu/scale/2")
            {
                ScalePoints = 4,
                ItemLabels = new []{ "Strongly Disagree", "Disagree", "Agree", "Strongly Agree" },
                ItemValues = new [] { -2, -1, 1, 2 }
            };

            JsonAssertions.AssertSameObjectJson(entity, "caliperEntityLikertScale");
        }

        [Test]
        public void EntityMultiselectScale_MatchesReferenceJson()
        {
            var entity = new MultiSelectScale("https://example.edu/scale/3")
            {
                ScalePoints = 5,
                ItemLabels = new [] { "😁", "😀", "😐", "😕", "😞" },
                ItemValues = new [] { "superhappy", "happy", "indifferent", "unhappy", "disappointed" },
                IsOrderedSelection = false,
                MinSelections = 1,
                MaxSelections = 5,
                DateCreated = CaliperTestEntities.Instant20180801060000
            };

            JsonAssertions.AssertSameObjectJson(entity, "caliperEntityMultiselectScale");
        }

        [Test]
        public void EntityNumericScale_MatchesReferenceJson()
        {
            var entity = new NumericScale("https://example.edu/scale/4")
            {
                MinValue = 0.0,
                MinLabel = "Disliked",
                MaxValue = 10.0,
                MaxLabel = "Liked",
                Step = 0.5,
                DateCreated = CaliperTestEntities.Instant20180801060000
            };

            JsonAssertions.AssertSameObjectJson(entity, "caliperEntityNumericScale", ignoreDefaultValues: false);
        }

        [Test]
        public void EntityQuestion_MatchesReferenceJson()
        {
            var entity = new Question("https://example.edu/question/1")
            {
                QuestionPosed = "How would you rate this?"
            };

            JsonAssertions.AssertSameObjectJson(entity, "caliperEntityQuestion");
        }

        [Test]
        public void EntityRatingScaleQuestion_MatchesReferenceJson()
        {
            var entity = new RatingScaleQuestion("https://example.edu/question/2")
            {
                QuestionPosed = "Do you agree with the opinion presented?",
                Scale = new LikertScale("https://example.edu/scale/2")
                {
                    ScalePoints = 4,
                    ItemLabels = new [] { "Strongly Disagree", "Disagree", "Agree", "Strongly Agree" },
                    ItemValues = new [] { -2, -1, 1, 2 },
                    DateCreated = CaliperTestEntities.Instant20180801060000,
                    HideCaliperContext = true
                }
            };

            JsonAssertions.AssertSameObjectJson(entity, "caliperEntityRatingScaleQuestion");
        }

        [Test]
        public void EventFeedbackCommented_MatchesReferenceJson()
        {
            var comment = new FeedbackComment("https://example.edu/terms/201801/courses/7/sections/1/assess/1/items/6/users/665544/responses/1/comment/1")
            {
                Commenter = CaliperTestEntities.Person554433(),
                CommentedOn = new DigitalResource("https://example.edu/terms/201801/courses/7/sections/1/resources/1/syllabus.pdf")
                {
                    Name = "Course Syllabus",
                    MediaType = "application/pdf",
                    IsPartOf = new DigitalResourceCollection("https://example.edu/terms/201801/courses/7/sections/1/resources/1", defaultContextV1p1)
                    {
                        Name = "Course Assets",
                        IsPartOf = new CourseSection("https://example.edu/terms/201801/courses/7/sections/1", defaultContextV1p1) { HideCaliperContext = true },
                        HideCaliperContext = true
                    },
                    DateCreated = Instant.FromUtc(2018, 08, 02, 11, 32, 00),
                    HideCaliperContext = true
                },
                Value = "I like what you did here but you need to improve on...",
                DateCreated = CaliperTestEntities.Instant20180801060000,
                HideCaliperContext = true
            };

            var feedbackEvent = new FeedbackEvent("urn:uuid:0c81f804-62ee-4953-81c5-62d9579c4369", comment)
            {
                Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
                Object = new DigitalResource("https://example.edu/terms/201801/courses/7/sections/1/resources/1/syllabus.pdf")
                {
                    Name = "Course Syllabus",
                    MediaType = "application/pdf",
                    IsPartOf = new DigitalResourceCollection("https://example.edu/terms/201801/courses/7/sections/1/resources/1", defaultContextV1p1)
                    {
                        Name = "Course Assets",
                        IsPartOf = new CourseSection("https://example.edu/terms/201801/courses/7/sections/1", defaultContextV1p1) { HideCaliperContext = true },
                        HideCaliperContext = true
                    },
                    DateCreated = Instant.FromUtc(2018, 08, 02, 11, 32, 00),
                    HideCaliperContext = true
                },
                EventTime = CaliperTestEntities.Instant20181115100500,
                EdApp = CaliperTestEntities.SoftwareAppV2(),
                Group = CaliperTestEntities.CourseSectionCPS43501Fall18(defaultContextV1p1),
                Membership = CaliperTestEntities.EntityMembership554433Learner_2018(defaultContextV1p1),
                Session = new Session("https://example.edu/sessions/1f6442a482de72ea6ad134943812bff564a76259", defaultContextV1p1)
                {
                    StartedAt = CaliperTestEntities.Instant20181115100000,
                    HideCaliperContext = true
                }
            };

            var coerced = JsonAssertions.coerce(feedbackEvent, new[] { "..membership.member", "..membership.organization", "..edApp" });

            JsonAssertions.AssertSameObjectJson(coerced, "caliperEventFeedbackCommented");
        }

        [Test]
        public void EventFeedbackRanked_MatchesReferenceJson()
        {
            var rating = new FeedbackRating("https://example.edu/users/554433/rating/1")
            {
                Rater = CaliperTestEntities.Person554433(),
                Rated = new DigitalResource("https://example.edu/terms/201801/courses/7/sections/1/resources/1/syllabus.pdf")
                {
                    Name = "Course Syllabus",
                    MediaType = "application/pdf",
                    IsPartOf = new DigitalResourceCollection("https://example.edu/terms/201801/courses/7/sections/1/resources/1", defaultContextV1p1)
                    {
                        Name = "Course Assets",
                        IsPartOf = new CourseSection("https://example.edu/terms/201801/courses/7/sections/1", defaultContextV1p1) { HideCaliperContext = true },
                        HideCaliperContext = true
                    },
                    DateCreated = Instant.FromUtc(2018, 08, 02, 11, 32, 00),
                    HideCaliperContext = true
                },
                Question = new RatingScaleQuestion("https://example.edu/question/2", defaultContextV1p1)
                {
                    QuestionPosed = "Do you agree with the opinion presented?",
                    Scale = new LikertScale("https://example.edu/scale/2")
                    {
                        ScalePoints = 4,
                        ItemLabels = new[] { "Strongly Disagree", "Disagree", "Agree", "Strongly Agree" },
                        ItemValues = new[] { -2, -1, 1, 2 },
                        HideCaliperContext = true
                    },
                    HideCaliperContext = true
                },
                Selections = new string[] { "1" },
                RatingComment = new FeedbackComment("https://example.edu/terms/201801/courses/7/sections/1/assess/1/items/6/users/665544/responses/1/comment/1")
                {
                    Commenter = CaliperTestEntities.Person554433(),
                    CommentedOn = new DigitalResource("https://example.edu/terms/201801/courses/7/sections/1/resources/1/syllabus.pdf")
                    {
                        Name = "Course Syllabus",
                        MediaType = "application/pdf",
                        IsPartOf = new DigitalResourceCollection("https://example.edu/terms/201801/courses/7/sections/1/resources/1", defaultContextV1p1)
                        {
                            Name = "Course Assets",
                            IsPartOf = new CourseSection("https://example.edu/terms/201801/courses/7/sections/1", defaultContextV1p1) { HideCaliperContext = true },
                            HideCaliperContext = true
                        },
                        DateCreated = Instant.FromUtc(2018, 08, 02, 11, 32, 00),
                        HideCaliperContext = true
                    },
                    Value = "I like what you did here but you need to improve on...",
                    DateCreated = CaliperTestEntities.Instant20180801060000,
                    HideCaliperContext = true
                },
                DateCreated = CaliperTestEntities.Instant20180801060000,
                HideCaliperContext = true
            };

            var feedbackEvent = new FeedbackEvent("urn:uuid:a502e4fc-24c1-11e9-ab14-d663bd873d93", rating)
            {
                Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
                Object = new DigitalResource("https://example.edu/terms/201801/courses/7/sections/1/resources/1/syllabus.pdf")
                {
                    Name = "Course Syllabus",
                    MediaType = "application/pdf",
                    IsPartOf = new DigitalResourceCollection("https://example.edu/terms/201801/courses/7/sections/1/resources/1", defaultContextV1p1)
                    {
                        Name = "Course Assets",
                        IsPartOf = new CourseSection("https://example.edu/terms/201801/courses/7/sections/1", defaultContextV1p1) { HideCaliperContext = true },
                        HideCaliperContext = true
                    },
                    DateCreated = Instant.FromUtc(2018, 08, 02, 11, 32, 00),
                    HideCaliperContext = true
                },
                EventTime = CaliperTestEntities.Instant20181115100500,
                EdApp = CaliperTestEntities.SoftwareAppV2(),
                Group = CaliperTestEntities.CourseSectionCPS43501Fall18(defaultContextV1p1),
                Membership = CaliperTestEntities.EntityMembership554433Learner_2018(defaultContextV1p1),
                Session = new Session("https://example.edu/sessions/1f6442a482de72ea6ad134943812bff564a76259", defaultContextV1p1)
                {
                    StartedAt = CaliperTestEntities.Instant20181115100000,
                    HideCaliperContext = true
                }
            };

            var coerced = JsonAssertions.coerce(feedbackEvent, new[] { "..membership.member", "..membership.organization", "..edApp" });

            JsonAssertions.AssertSameObjectJson(coerced, "caliperEventFeedbackRanked");
        }

        [Test]
        public void EntityRating_MatchesReferenceJson()
        {
            var rating = new FeedbackRating("https://example.edu/users/554433/rating/1")
            {
                Rater = CaliperTestEntities.Person554433(),
                Rated = new DigitalResource("https://example.edu/terms/201801/courses/7/sections/1/resources/1/syllabus.pdf")
                {
                    Name = "Course Syllabus",
                    MediaType = "application/pdf",
                    IsPartOf = new DigitalResourceCollection("https://example.edu/terms/201801/courses/7/sections/1/resources/1", defaultContextV1p1)
                    {
                        Name = "Course Assets",
                        IsPartOf = new CourseSection("https://example.edu/terms/201801/courses/7/sections/1", defaultContextV1p1) { HideCaliperContext = true },
                        HideCaliperContext = true
                    },
                    DateCreated = Instant.FromUtc(2018, 08, 02, 11, 32, 00),
                    HideCaliperContext = true
                },
                Question = new RatingScaleQuestion("https://example.edu/question/2", defaultContextV1p1)
                {
                    QuestionPosed = "Do you agree with the opinion presented?",
                    Scale = new LikertScale("https://example.edu/scale/2")
                    {
                        ScalePoints = 4,
                        ItemLabels = new[] { "Strongly Disagree", "Disagree", "Agree", "Strongly Agree" },
                        ItemValues = new[] { -2, -1, 1, 2 },
                        HideCaliperContext = true
                    },
                    HideCaliperContext = true
                },
                Selections = new string[] { "1" },
                RatingComment = new FeedbackComment("https://example.edu/terms/201801/courses/7/sections/1/assess/1/items/6/users/665544/responses/1/comment/1")
                {
                    Commenter = CaliperTestEntities.Person554433(),
                    CommentedOn = new DigitalResource("https://example.edu/terms/201801/courses/7/sections/1/resources/1/syllabus.pdf")
                    {
                        Name = "Course Syllabus",
                        MediaType = "application/pdf",
                        IsPartOf = new DigitalResourceCollection("https://example.edu/terms/201801/courses/7/sections/1/resources/1", defaultContextV1p1)
                        {
                            Name = "Course Assets",
                            IsPartOf = new CourseSection("https://example.edu/terms/201801/courses/7/sections/1", defaultContextV1p1) { HideCaliperContext = true },
                            HideCaliperContext = true
                        },
                        DateCreated = Instant.FromUtc(2018, 08, 02, 11, 32, 00),
                        HideCaliperContext = true
                    },
                    Value = "I like what you did here but you need to improve on...",
                    DateCreated = CaliperTestEntities.Instant20180801060000,
                    HideCaliperContext = true
                },
                DateCreated = CaliperTestEntities.Instant20180801060000
            };

            JsonAssertions.AssertSameObjectJson(rating, "caliperEntityRating");
        }

        [Test]
        public void EventResourceManagementPrinted_MatchesReferenceJson()
        {
            var resourceManagement = new ResourceManagementEvent("urn:uuid:d3543a73-e307-4190-a755-5ce7b3187bc5", Action.Printed)
            {
                Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
                Object = new DigitalResource("https://example.edu/terms/201801/courses/7/sections/1/resources/1/syllabus.pdf")
                {
                    Name = "Course Syllabus",
                    MediaType = "application/pdf",
                    Creators = new[] { new Person("https://example.edu/users/554433", defaultContextV1p1) { HideCaliperContext = true } },
                    IsPartOf = new DigitalResourceCollection("https://example.edu/terms/201801/courses/7/sections/1/resources/1", defaultContextV1p1)
                    {
                        Name = "Course Assets",
                        IsPartOf = new CourseSection("https://example.edu/terms/201801/courses/7/sections/1", defaultContextV1p1) { HideCaliperContext = true },
                        HideCaliperContext = true
                    },
                    DateCreated = Instant.FromUtc(2018, 08, 02, 11, 32, 00),
                    HideCaliperContext = true
                },
                EventTime = CaliperTestEntities.Instant20181115100500,
                EdApp = CaliperTestEntities.SoftwareAppV2(),
                Group = CaliperTestEntities.CourseSectionCPS43501Fall18(defaultContextV1p1),
                Membership = new Membership("https://example.edu/terms/201801/courses/7/sections/1/rosters/1", defaultContextV1p1)
                {
                    Member = new Person("https://example.edu/users/554433", defaultContextV1p1),
                    Organization = new Organization("https://example.edu/terms/201801/courses/7/sections/1", defaultContextV1p1),
                    Roles = new[] { Role.Instructor },
                    Status = Status.Active,
                    DateCreated = CaliperTestEntities.Instant20180801060000,
                    HideCaliperContext = true
                },
                Session = new Session("https://example.edu/sessions/1f6442a482de72ea6ad134943812bff564a76259", defaultContextV1p1)
                {
                    StartedAt = CaliperTestEntities.Instant20181115100000,
                    HideCaliperContext = true
                }
            };

            var coerced = JsonAssertions.coerce(resourceManagement, new[] { "..membership.member", "..membership.organization", "..edApp" });

            JsonAssertions.AssertSameObjectJson(coerced, "caliperEventResourceManagementPrinted");
        }

        [Test]
        public void EventResourceManagementCreated_MatchesReferenceJson()
        {
            var resourceManagement = new ResourceManagementEvent("urn:uuid:0c81f804-62ee-4953-81c5-62d9579c4369", Action.Created)
            {
                Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
                Object = new DigitalResource("https://example.edu/terms/201801/courses/7/sections/1/resources/1/syllabus.pdf")
                {
                    Name = "Course Syllabus",
                    MediaType = "application/pdf",
                    Creators = new[] { new Person("https://example.edu/users/554433", defaultContextV1p1) { HideCaliperContext = true } },
                    IsPartOf = new DigitalResourceCollection("https://example.edu/terms/201801/courses/7/sections/1/resources/1", defaultContextV1p1)
                    {
                        Name = "Course Assets",
                        IsPartOf = new CourseSection("https://example.edu/terms/201801/courses/7/sections/1", defaultContextV1p1) { HideCaliperContext = true },
                        HideCaliperContext = true
                    },
                    DateCreated = Instant.FromUtc(2018, 08, 02, 11, 32, 00),
                    HideCaliperContext = true
                },
                EventTime = CaliperTestEntities.Instant20181115100500,
                EdApp = CaliperTestEntities.SoftwareAppV2(),
                Group = CaliperTestEntities.CourseSectionCPS43501Fall18(defaultContextV1p1),
                Membership = new Membership("https://example.edu/terms/201801/courses/7/sections/1/rosters/1", defaultContextV1p1)
                {
                    Member = new Person("https://example.edu/users/554433", defaultContextV1p1),
                    Organization = new Organization("https://example.edu/terms/201801/courses/7/sections/1", defaultContextV1p1),
                    Roles = new[] { Role.Instructor },
                    Status = Status.Active,
                    DateCreated = CaliperTestEntities.Instant20180801060000,
                    HideCaliperContext = true
                },
                Session = new Session("https://example.edu/sessions/1f6442a482de72ea6ad134943812bff564a76259", defaultContextV1p1)
                {
                    StartedAt = CaliperTestEntities.Instant20181115100000,
                    HideCaliperContext = true
                }
            };

            var coerced = JsonAssertions.coerce(resourceManagement, new[] { "..membership.member", "..membership.organization", "..edApp" });

            JsonAssertions.AssertSameObjectJson(coerced, "caliperEventResourceManagementCreated");
        }

        [Test]
        public void EventResourceManagementCopied_MatchesReferenceJson()
        {
            var resourceManagement = new ResourceManagementEvent("urn:uuid:d3543a73-e307-4190-a755-5ce7b3187bc5", Action.Copied)
            {
                Actor = CaliperTestEntities.Person554433(defaultContextV1p1),
                Object = new DigitalResource("https://example.edu/terms/201801/courses/7/sections/1/resources/1/syllabus.pdf")
                {
                    Name = "Course Syllabus",
                    MediaType = "application/pdf",
                    Creators = new[] { new Person("https://example.edu/users/554433", defaultContextV1p1) { HideCaliperContext = true } },
                    IsPartOf = new DigitalResourceCollection("https://example.edu/terms/201801/courses/7/sections/1/resources/1", defaultContextV1p1)
                    {
                        Name = "Course Assets",
                        IsPartOf = new CourseSection("https://example.edu/terms/201801/courses/7/sections/1", defaultContextV1p1) { HideCaliperContext = true },
                        HideCaliperContext = true
                    },
                    DateCreated = Instant.FromUtc(2018, 08, 02, 11, 32, 00),
                    HideCaliperContext = true
                },
                Generated = new DigitalResource("https://example.edu/terms/201801/courses/7/sections/1/resources/1/syllabus_copy.pdf")
                {
                    Name = "Course Syllabus (copy)",
                    MediaType = "application/pdf",
                    Creators = new[] { new Person("https://example.edu/users/554433", defaultContextV1p1) { HideCaliperContext = true } },
                    IsPartOf = new DigitalResourceCollection("https://example.edu/terms/201801/courses/7/sections/1/resources/1", defaultContextV1p1)
                    {
                        Name = "Course Assets",
                        IsPartOf = new CourseSection("https://example.edu/terms/201801/courses/7/sections/1", defaultContextV1p1) { HideCaliperContext = true },
                        HideCaliperContext = true
                    },
                    DateCreated = CaliperTestEntities.Instant20181115100500,
                    HideCaliperContext = true
                },
                EventTime = CaliperTestEntities.Instant20181115100500,
                EdApp = CaliperTestEntities.SoftwareAppV2(),
                Group = CaliperTestEntities.CourseSectionCPS43501Fall18(defaultContextV1p1),
                Membership = new Membership("https://example.edu/terms/201801/courses/7/sections/1/rosters/1", defaultContextV1p1)
                {
                    Member = new Person("https://example.edu/users/554433", defaultContextV1p1),
                    Organization = new Organization("https://example.edu/terms/201801/courses/7/sections/1", defaultContextV1p1),
                    Roles = new[] { Role.Instructor },
                    Status = Status.Active,
                    DateCreated = CaliperTestEntities.Instant20180801060000,
                    HideCaliperContext = true
                },
                Session = new Session("https://example.edu/sessions/1f6442a482de72ea6ad134943812bff564a76259", defaultContextV1p1)
                {
                    StartedAt = CaliperTestEntities.Instant20181115100000,
                    HideCaliperContext = true
                }
            };

            var coerced = JsonAssertions.coerce(resourceManagement, new[] { "..membership.member", "..membership.organization", "..edApp" });

            JsonAssertions.AssertSameObjectJson(coerced, "caliperEventResourceManagementCopied");
        }
    }
}
