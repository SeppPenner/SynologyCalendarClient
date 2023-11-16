// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeserializationTests.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//  Tests the deserialization of models.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynologyCalendarClient.Test;

/// <summary>
/// Tests the deserialization of models.
/// </summary>
[TestClass]
public class DeserializationTests
{
    #region Login
    /// <summary>
    /// Tests the deserialization of the success response of a login result.
    /// </summary>
    [TestMethod]
    public void TestSuccessLoginResult()
    {
        var dataString = @"{
	        ""data"": {
		        ""sid"": ""ZU6dNe8YMIPVo15A0NJN507300"",
		        ""synotoken"": ""iUahVw8TG.Uhc""
	        },
	        ""success"": true
        }";

        var data = JsonConvert.DeserializeObject<Result<LoginResult, LoginErrorCode>?>(dataString);

        Assert.IsNotNull(data);
        Assert.IsNotNull(data.Data);
        Assert.AreEqual("ZU6dNe8YMIPVo15A0NJN507300", data.Data.Sid);
        Assert.AreEqual("iUahVw8TG.Uhc", data.Data.SynoToken);
        Assert.IsTrue(data.Success);
    }

    /// <summary>
    /// Tests the deserialization of the error response of a login result.
    /// </summary>
    [TestMethod]
    public void TestErrorLoginResult()
    {
        var dataString = @"{
	        ""success"": false,
	        ""error"": {
		        ""code"": 402
	        }
        }";

        var data = JsonConvert.DeserializeObject<Result<LoginResult, LoginErrorCode>?>(dataString);

        Assert.IsNotNull(data);
        Assert.IsNull(data.Data);
        Assert.IsFalse(data.Success);
        Assert.IsNotNull(data.ErrorData);
        Assert.AreEqual(LoginErrorCode.PermissionDenied, data.ErrorData.ErrorCode);
    }
    #endregion Login

    #region Logout
    /// <summary>
    /// Tests the deserialization of the success response of a logout result.
    /// </summary>
    [TestMethod]
    public void TestSuccessLogoutResult()
    {
        var dataString = @"{
	        ""success"": true,
	        ""error"": {
	        }
        }";

        var data = JsonConvert.DeserializeObject<Result<LogoutResult, CommonErrorCode>?>(dataString);

        Assert.IsNotNull(data);
        Assert.IsNull(data.Data);
        Assert.IsTrue(data.Success);
        Assert.IsNull(data.ErrorData);
    }

    /// <summary>
    /// Tests the deserialization of the error response of a logout result.
    /// </summary>
    [TestMethod]
    public void TestErrorLogoutResult()
    {
        var dataString = @"{
	        ""success"": false,
	        ""error"": {
		        ""code"": 114
	        }
        }";

        var data = JsonConvert.DeserializeObject<Result<LogoutResult, CommonErrorCode>?>(dataString);

        Assert.IsNotNull(data);
        Assert.IsNull(data.Data);
        Assert.IsFalse(data.Success);
        Assert.IsNotNull(data.ErrorData);
        Assert.AreEqual(CommonErrorCode.MissingParameters, data.ErrorData.ErrorCode);
    }
    #endregion Logout

    #region Info
    /// <summary>
    /// Tests the deserialization of the success response of an information result.
    /// </summary>
    [TestMethod]
    public void TestInfoResult()
    {
        var dataString = @"{
	        ""data"": {
		        ""SYNO.API.Auth"": {
			        ""path"": ""auth.cgi"",
			        ""minVersion"": 1,
			        ""maxVersion"": 3
		        },
		        ""SYNO.FileStation.List"": {
			        ""path"": ""FileStation/file_share.cgi"",
			        ""minVersion"": 1,
			        ""maxVersion"": 1
		        }
	        },
	        ""success"": true
        }";

        var data = JsonConvert.DeserializeObject<Result<Dictionary<string, InfoResult>, CommonErrorCode>?>(dataString);

        Assert.IsNotNull(data);
        Assert.IsNotNull(data.Data);
        Assert.IsNotNull(data.Data["SYNO.API.Auth"]);
        Assert.AreEqual("auth.cgi", data.Data["SYNO.API.Auth"].Path);
        Assert.AreEqual(1, data.Data["SYNO.API.Auth"].MinimumVersion);
        Assert.AreEqual(3, data.Data["SYNO.API.Auth"].MaximumVersion);
        Assert.IsNotNull(data.Data["SYNO.FileStation.List"]);
        Assert.AreEqual("FileStation/file_share.cgi", data.Data["SYNO.FileStation.List"].Path);
        Assert.AreEqual(1, data.Data["SYNO.FileStation.List"].MinimumVersion);
        Assert.AreEqual(1, data.Data["SYNO.FileStation.List"].MaximumVersion);
        Assert.IsTrue(data.Success);
    }
    #endregion Info

    #region Calendar
    /// <summary>
    /// Tests the deserialization of the success response of a create calendar result.
    /// </summary>
    [TestMethod]
    public void TestCreateCalendar()
    {
        var dataString = @"{
	        ""data"": {
		        ""cal_color"": ""#F94B4B"",
		        ""cal_description"": """",
		        ""cal_displayname"": ""Untitled"",
		        ""cal_extra_info"": ""{}"",
		        ""cal_id"": ""/admin/tlvjj/"",
		        ""cal_privilege"": ""RW"",
		        ""cal_public_sharing_id"": ""cO9KZOHlK"",
		        ""create_time"": ""2019-06-28 11:59:17.160673+00"",
		        ""is_evt"": true,
		        ""is_hidden_in_cal"": false,
		        ""is_hidden_in_list"": false,
		        ""modify_time"": ""2019-06-28 11:59:17.160673+00"",
		        ""notify_alarm_by_browser"": true,
		        ""notify_alarm_by_mail"": true,
		        ""notify_daily_agenda"": """",
		        ""notify_evt_by_browser"": true,
		        ""notify_evt_by_mail"": false,
		        ""notify_evt_create"": """",
		        ""notify_evt_delete"": """",
		        ""notify_evt_rsvp"": """",
		        ""notify_evt_set"": """",
		        ""notify_import_cal_by_browser"": true,
		        ""notify_import_cal_by_mail"": true,
		        ""original_cal_id"": ""/admin/tlvjj/"",
		        ""ug_name"": ""admin"",
		        ""user_no"": 1024
	        },
	        ""success"": true
        }";

        var data = JsonConvert.DeserializeObject<Result<CalendarResult, CommonErrorCode>?>(dataString);

        Assert.IsNotNull(data);
        Assert.IsNotNull(data.Data);
        Assert.AreEqual("#F94B4B", data.Data.CalendarColor);
        Assert.AreEqual(string.Empty, data.Data.CalendarDescription);
        Assert.AreEqual("Untitled", data.Data.CalendarDisplayName);
        Assert.AreEqual("{}", data.Data.CalendarExtraInformation);
        Assert.AreEqual("/admin/tlvjj/", data.Data.CalendarId);
        Assert.AreEqual("RW", data.Data.Privilege);
        Assert.AreEqual("cO9KZOHlK", data.Data.PublicSharingId);
        Assert.AreEqual("2019-06-28 11:59:17.160673+00", data.Data.CreateTime);
        Assert.IsTrue(data.Data.IsEvent);
        Assert.IsFalse(data.Data.IsHiddenInCalendar);
        Assert.IsFalse(data.Data.IsHiddenInList);
        Assert.AreEqual("2019-06-28 11:59:17.160673+00", data.Data.ModifyTime);
        Assert.IsFalse(data.Data.NotifyAlarmByBrowser);
        Assert.IsFalse(data.Data.NotifyAlarmByMail);
        Assert.AreEqual(string.Empty, data.Data.NotifyDailyAgenda);
        Assert.IsTrue(data.Data.NotifyEventByBrowser);
        Assert.IsFalse(data.Data.NotifyEventByMail);
        Assert.AreEqual(string.Empty, data.Data.NotifyEventCreate);
        Assert.AreEqual(string.Empty, data.Data.NotifyEventDelete);
        Assert.AreEqual(string.Empty, data.Data.NotifyEventRsvp);
        Assert.AreEqual(string.Empty, data.Data.NotifyEventSet);
        Assert.IsTrue(data.Data.NotifyImportCalendarByBrowser);
        Assert.IsTrue(data.Data.NotifyImportCalendarByMail);
        Assert.AreEqual("/admin/tlvjj/", data.Data.OriginalCalendarId);
        Assert.AreEqual("admin", data.Data.UserGroup);
        Assert.AreEqual(1024, data.Data.UserNumber);
        Assert.IsTrue(data.Success);
    }

    /// <summary>
    /// Tests the deserialization of the success response of a list calendars result.
    /// </summary>
    [TestMethod]
    public void TestListCalendars()
    {
        var dataString = @"{
	        ""data"": [
		        {
			        ""cal_color"": ""#FB0055FF"",
			        ""cal_description"": """",
			        ""cal_displayname"": ""My Calendar"",
			        ""cal_id"": ""/admin/home/"",
			        ""cal_order"": ""1"",
			        ""cal_privilege"": ""RW"",
			        ""create_time"": 1473070714.34871,
			        ""default_calendar"": true,
			        ""is_evt"": true,
			        ""is_hidden_in_cal"": false,
			        ""is_hidden_in_list"": false,
			        ""modify_time"": 1561720485.77791,
			        ""notify_alarm_by_browser"": true,
			        ""notify_alarm_by_mail"": true,
			        ""notify_daily_agenda"": """",
			        ""notify_evt_by_browser"": true,
			        ""notify_evt_by_mail"": false,
			        ""notify_evt_create"": """",
			        ""notify_evt_delete"": """",
			        ""notify_evt_rsvp"": """",
			        ""notify_evt_set"": """",
			        ""notify_import_cal_by_browser"": true,
			        ""notify_import_cal_by_mail"": true,
			        ""original_cal_id"": ""/admin/home/"",
			        ""ug_name"": ""admin"",
			        ""user_no"": 1024
		        }
	        ],
	        ""success"": true
        }";

        var data = JsonConvert.DeserializeObject<Result<List<CalendarResult>, CommonErrorCode>?>(dataString);

        Assert.IsNotNull(data);
        Assert.IsNotNull(data.Data);
        Assert.AreEqual(1, data.Data.Length);
        Assert.IsNotNull(data.Data[0]);
        Assert.AreEqual("#FB0055FF", data.Data[0].CalendarColor);
        Assert.AreEqual(string.Empty, data.Data[0].CalendarDescription);
        Assert.AreEqual("My Calendar", data.Data[0].CalendarDisplayName);
        Assert.AreEqual("/admin/home/", data.Data[0].CalendarId);
        Assert.AreEqual("1", data.Data[0].CalendarOrder);
        Assert.AreEqual("RW", data.Data[0].Privilege);
        Assert.AreEqual("1473070714.34871", data.Data[0].CreateTime);
        Assert.IsTrue(data.Data[0].DefaultCalendar);
        Assert.IsTrue(data.Data[0].IsEvent);
        Assert.IsFalse(data.Data[0].IsHiddenInCalendar);
        Assert.IsFalse(data.Data[0].IsHiddenInList);
        Assert.AreEqual("1561720485.77791", data.Data[0].ModifyTime);
        Assert.IsTrue(data.Data[0].NotifyAlarmByBrowser);
        Assert.IsTrue(data.Data[0].NotifyAlarmByMail);
        Assert.AreEqual(string.Empty, data.Data[0].NotifyDailyAgenda);
        Assert.IsTrue(data.Data[0].NotifyEventByBrowser);
        Assert.IsFalse(data.Data[0].NotifyEventByMail);
        Assert.AreEqual(string.Empty, data.Data[0].NotifyEventCreate);
        Assert.AreEqual(string.Empty, data.Data[0].NotifyEventDelete);
        Assert.AreEqual(string.Empty, data.Data[0].NotifyEventRsvp);
        Assert.AreEqual(string.Empty, data.Data[0].NotifyEventSet);
        Assert.IsTrue(data.Data[0].NotifyImportCalendarByBrowser);
        Assert.IsTrue(data.Data[0].NotifyImportCalendarByMail);
        Assert.AreEqual("/admin/home/", data.Data[0].OriginalCalendarId);
        Assert.AreEqual("admin", data.Data[0].UserGroup);
        Assert.AreEqual(1024, data.Data[0].UserNumber);
        Assert.IsTrue(data.Success);
    }

    /// <summary>
    /// Tests the deserialization of the success response of a get or set calendar result.
    /// </summary>
    [TestMethod]
    public void TestGetOrSetCalendar()
    {
        var dataString = @"{
	        ""data"": {
			    ""cal_color"": ""#FB0055FF"",
			    ""cal_description"": """",
			    ""cal_displayname"": ""My Calendar"",
			    ""cal_id"": ""/admin/home/"",
			    ""cal_order"": ""1"",
			    ""cal_privilege"": ""RW"",
			    ""create_time"": 1473070714.34871,
			    ""default_calendar"": true,
			    ""is_evt"": true,
			    ""is_hidden_in_cal"": false,
			    ""is_hidden_in_list"": false,
			    ""modify_time"": 1561720485.77791,
			    ""notify_alarm_by_browser"": true,
			    ""notify_alarm_by_mail"": true,
			    ""notify_daily_agenda"": """",
			    ""notify_evt_by_browser"": true,
			    ""notify_evt_by_mail"": false,
			    ""notify_evt_create"": """",
			    ""notify_evt_delete"": """",
			    ""notify_evt_rsvp"": """",
			    ""notify_evt_set"": """",
			    ""notify_import_cal_by_browser"": true,
			    ""notify_import_cal_by_mail"": true,
			    ""original_cal_id"": ""/admin/home/"",
			    ""ug_name"": ""admin"",
			    ""user_no"": 1024
		    },
	        ""success"": true
        }";

        var data = JsonConvert.DeserializeObject<Result<CalendarResult, CommonErrorCode>?>(dataString);

        Assert.IsNotNull(data);
        Assert.IsNotNull(data.Data);
        Assert.AreEqual("#FB0055FF", data.Data.CalendarColor);
        Assert.AreEqual(string.Empty, data.Data.CalendarDescription);
        Assert.AreEqual("My Calendar", data.Data.CalendarDisplayName);
        Assert.AreEqual("/admin/home/", data.Data.CalendarId);
        Assert.AreEqual("1", data.Data.CalendarOrder);
        Assert.AreEqual("RW", data.Data.Privilege);
        Assert.AreEqual("1473070714.34871", data.Data.CreateTime);
        Assert.IsTrue(data.Data.DefaultCalendar);
        Assert.IsTrue(data.Data.IsEvent);
        Assert.IsFalse(data.Data.IsHiddenInCalendar);
        Assert.IsFalse(data.Data.IsHiddenInList);
        Assert.AreEqual("1561720485.77791", data.Data.ModifyTime);
        Assert.IsTrue(data.Data.NotifyAlarmByBrowser);
        Assert.IsTrue(data.Data.NotifyAlarmByMail);
        Assert.AreEqual(string.Empty, data.Data.NotifyDailyAgenda);
        Assert.IsTrue(data.Data.NotifyEventByBrowser);
        Assert.IsFalse(data.Data.NotifyEventByMail);
        Assert.AreEqual(string.Empty, data.Data.NotifyEventCreate);
        Assert.AreEqual(string.Empty, data.Data.NotifyEventDelete);
        Assert.AreEqual(string.Empty, data.Data.NotifyEventRsvp);
        Assert.AreEqual(string.Empty, data.Data.NotifyEventSet);
        Assert.IsTrue(data.Data.NotifyImportCalendarByBrowser);
        Assert.IsTrue(data.Data.NotifyImportCalendarByMail);
        Assert.AreEqual("/admin/home/", data.Data.OriginalCalendarId);
        Assert.AreEqual("admin", data.Data.UserGroup);
        Assert.AreEqual(1024, data.Data.UserNumber);
        Assert.IsTrue(data.Success);
    }

    /// <summary>
    /// Tests the deserialization of the success response of a delete calendar result.
    /// </summary>
    [TestMethod]
    public void TestDeleteCalendar()
    {
        var dataString = @"{
	        ""success"": true
        }";

        var data = JsonConvert.DeserializeObject<Result<CalendarResult, CommonErrorCode>?>(dataString);

        Assert.IsNotNull(data);
        Assert.IsNull(data.Data);
        Assert.IsTrue(data.Success);
    }
    #endregion Calendar
}
