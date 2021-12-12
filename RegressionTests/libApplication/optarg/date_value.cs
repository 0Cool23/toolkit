/** @file

    @copyright  @ref TESTING_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)
*/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;

using libApplication.arg;
using libApplication.optarg;

namespace RegressionTests.libApplication.optarg
{
/** @ingroup GRP_Testing

    @class   DateValue_test

    @details Unit test for @ref REF_DateValue class.
*/
[TestClass]
//[Ignore]
public class DateValue_test
    {
    /** @test Test to make sure that DateValue defaults are properly set after construction.
    */
    [TestMethod]
    //[Ignore]
    public void construction()
        {
        var date_value = new DateValue();
        Assert.AreEqual(DateTime.ParseExact(DateValue.DEFAULT_TIMESTAMP, DateValue.DEFAULT_DATE_FORMAT, CultureInfo.InvariantCulture), date_value.Timestamp, "Timestamp property did not match expected defaults.");
        }

    /** @test Test to make sure that DateValue parses string "now" properly.
    */
    [TestMethod]
    //[Ignore]
    public void timestamp_now()
        {
        var date_value = new DateValue();
        date_value.parse("now");
        var timestamp = DateTime.Now;
        var timespan  = timestamp.Subtract(date_value.Timestamp);
        var elapsed_ticks = timespan.Ticks; // One tick is 100ns
        Assert.IsTrue(elapsed_ticks <= 10000, "Elapsed ticks for now should be less than 10000 ticks, but got {0}.", elapsed_ticks);
        }

    /** @test Test to make sure that DateValue parses string "now" properly.
 
        @note This test might fail if it is started short before a change to the next day. It
              is possible to delay this test in case we are very close to a day change to
              prevent failure in any case.
    */
    [TestMethod]
    //[Ignore]
    public void timestamp_today()
        {
        var date_value = new DateValue();
        date_value.parse("today");
        var timestamp = DateTime.Today;
        var timespan  = timestamp.Subtract(date_value.Timestamp);
        var elapsed_ticks = timespan.Ticks; // One tick is 100ns
        Assert.IsTrue(elapsed_ticks == 0, "Elapsed ticks for now should be less than 0 ticks, but got {0}.", elapsed_ticks);
        }

    /** @test Test to make sure that DateValue will throw an exception if the string value is not a translatable date.
    */
    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "ArgumentException expected when passed date string is not translatable.")]
    //[Ignore]
    public void timestamp_exception()
        {
        var date_value = new DateValue();
        date_value.parse("not translatable");
        }

    private static readonly Dictionary<string, DateTime> the_datetime_map = new()
        {
            {"1972-05-27",                new DateTime(1972, 5, 27,  0,  0,  0,   0, CultureInfo.InvariantCulture.Calendar, DateTimeKind.Utc)},
            {"1972-05-27+02:00",          new DateTime(1972, 5, 26, 22,  0,  0,   0, CultureInfo.InvariantCulture.Calendar, DateTimeKind.Utc)},
            {"1972-05-27+00:00",          new DateTime(1972, 5, 27,  0,  0,  0,   0, CultureInfo.InvariantCulture.Calendar, DateTimeKind.Utc)},
            {"1972-05-27T17:58:34",       new DateTime(1972, 5, 27, 17, 58, 34,   0, CultureInfo.InvariantCulture.Calendar, DateTimeKind.Utc)},
            {"1972-05-27T17:58:34.987",   new DateTime(1972, 5, 27, 17, 58, 34, 987, CultureInfo.InvariantCulture.Calendar, DateTimeKind.Utc)},
            {"1972-05-27T13:58:46+02:00", new DateTime(1972, 5, 27, 11, 58, 46,   0, CultureInfo.InvariantCulture.Calendar, DateTimeKind.Utc)},
            {"1972-05-27T13:58:46+00:00", new DateTime(1972, 5, 27, 13, 58, 46,   0, CultureInfo.InvariantCulture.Calendar, DateTimeKind.Utc)},
        };

    /** @test Test to make sure that DateValue parsed valid values correctly.
    */
    [TestMethod]
    //[Ignore]
    public void timestamp_pass()
        {
        var date_value = new DateValue();
        foreach( var datetime_entry in the_datetime_map )
            {
            date_value.parse(datetime_entry.Key);
            Assert.AreEqual(datetime_entry.Value, date_value.Timestamp, "Timestamp value did not match expected result for '{0}'.", datetime_entry.Key);
            }
        }
    }
}
