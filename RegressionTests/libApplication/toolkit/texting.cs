/** @file

    @copyright  @ref TESTING_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)
*/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

using libApplication.toolkit;

namespace RegressionTests.libApplication.toolkit
{
/** @ingroup REF_Testing REF_Texting

    @class   Texting_test

    @brief   Unit test for @ref REF_Texting class.
*/
[TestClass]
//[Ignore]
public class Texting_test
    {
    /** @test Test LIBRARY_NAME string matches expected string value.
    */
    [TestMethod]
    //[Ignore]
    public void library_name()
        {
        Assert.AreEqual("libApplication", Texting.LIBRARY_NAME, "LIBRARY_NAME did not match expected value.");
        }

    /** @test Test is_digit_char method returns correct result boolean result.
    */
    [TestMethod]
    //[Ignore]
    public void is_digit_char_range()
        {
        Assert.IsFalse(Texting.is_digit_char('0', '1', '2'), "Digit '0' is not expected to be in ['1'..'2']");
        Assert.IsTrue(Texting.is_digit_char('1', '1', '2'),  "Digit '1' is expected to be in ['1'..'2']");
        Assert.IsTrue(Texting.is_digit_char('2', '1', '2'),  "Digit '2' is expected to be in ['1'..'2']");
        Assert.IsFalse(Texting.is_digit_char('3', '1', '2'), "Digit '3' is not expected to be in ['1'..'2']");
        }

    /** @test Test is_digit_char method returns correct result boolean result.
    */
    [TestMethod]
    //[Ignore]
    public void is_digit_char_max()
        {
        foreach( var digit in "0123456789" )
            {
            Assert.IsTrue(Texting.is_digit_char(digit), "Digit is expected to be in ['0'..'9']");
            }

        Assert.IsTrue(Texting.is_digit_char('0', '0'), "Digit '0' is expected to be in ['0'..'0']");
        foreach( var digit in "123456789" )
            {
            Assert.IsFalse(Texting.is_digit_char(digit, '0'), "Digit is not expected to be in ['0'..'0']");
            }
        }

    /** @test Test is_digit_string method returns correct result boolean result.
    */
    [TestMethod]
    //[Ignore]
    public void is_digit_string()
        {
        Assert.IsFalse(Texting.is_digit_string(null),           "is_digit_string must return false for null value.");
        Assert.IsFalse(Texting.is_digit_string(string.Empty),   "is_digit_string must return false for empty string value.");
        Assert.IsTrue(Texting.is_digit_string("01234567890"),   "is_digit_string must return true for numeric string value.");
        Assert.IsFalse(Texting.is_digit_string("a01234567890"), "is_digit_string must return false for string with leading non numeric character.");
        Assert.IsFalse(Texting.is_digit_string("0123456b7890"), "is_digit_string must return false for string containing non numeric character.");
        Assert.IsFalse(Texting.is_digit_string("01234567890c"), "is_digit_string must return false for string with trailing non numeric character.");

        Assert.IsFalse(Texting.is_digit_string("12a34567890", 11), "is_digit_string (11) with invalid character and expected length.");
        Assert.IsFalse(Texting.is_digit_string("1234567890", 3),   "is_digit_string (3) does not have expected length.");
        Assert.IsFalse(Texting.is_digit_string("1234567890", 11),  "is_digit_string (11) does not have expected length.");
        Assert.IsTrue(Texting.is_digit_string("1234567890", 10),   "is_digit_string (10) has expected length.");
        }

    /** @test Test to ensure is_printable_string will return correct results:
              - Will return false for null string.
              - Will return false for empty string.
              - Will return true for printable special non standard ASCII character single string values.
              - Will return true for strings which contain only printable characters.
              - Will return false for whitespace characters as single character strings.
              - Will return false for strings which contain whitespace characters.
    */
    [TestMethod]
    //[Ignore]
    public void is_printable_string()
        {
        Assert.AreEqual<bool>(false, Texting.is_printable_string(null));
        Assert.AreEqual<bool>(false, Texting.is_printable_string(string.Empty));

        List<char> printables = new()
            {
            '0', 'a', 'ä', 'Ö', 'ß', '?', ' '
            };
        foreach( var ch in printables )
            {
            Assert.AreEqual<bool>(true, Texting.is_printable_string(new string(ch, 1)));
            }

        Assert.AreEqual<bool>(true, Texting.is_printable_string("0abjsfasdf ajkölkj!!!###äöüÄÖÜßß."));

        List<char> non_printables = new()
            {
            '\t', '\r', '\n'
            };
        foreach( var ch in non_printables )
            {
            Assert.AreEqual<bool>(false, Texting.is_printable_string(new string(ch, 1)));
            }

        Assert.AreEqual<bool>(false, Texting.is_printable_string("\t0abjsfasdfajkölkj!!!###äöüÄÖÜßß."));
        Assert.AreEqual<bool>(false, Texting.is_printable_string("0abjsfasdfajkölkj!!!###äöüÄÖÜßß.\v"));
        Assert.AreEqual<bool>(false, Texting.is_printable_string("0abjsfasdfajk\nölkj!!!###äöüÄÖÜßß."));

        Assert.IsFalse(Texting.is_printable_string("\t", 1), "");
        Assert.IsTrue(Texting.is_printable_string("1", 1), "");
        }

    /** @test Test to ensure is_printable_string_or_empty will return correct results:
              - Will return false for null string.
              - Will return true for empty string.
              - Will return false for valid string exceeding maximum length.
              - Will return true for valid string with maximum length.
    */
    [TestMethod]
    //[Ignore]
    public void is_printable_string_or_empty()
        {
        Assert.IsFalse(Texting.is_printable_string_or_empty(null, 0), "");
        Assert.IsTrue(Texting.is_printable_string_or_empty(string.Empty, 0), "");
        Assert.IsFalse(Texting.is_printable_string_or_empty("1", 0), "");
        Assert.IsTrue(Texting.is_printable_string_or_empty("1", 1), "");
        }

    /** @test Test to ensure remove_leading_zeros returns null.
    */
    [TestMethod]
    //[Ignore]
    public void remove_leading_zeros_null()
        {
        Assert.IsNull(Texting.remove_leading_zeros(null), "remove_leading_zeros must return null when input argument is a null value.");
        }

    /** @test Test to ensure remove_leading_zeros returns an empty string.
    */
    [TestMethod]
    //[Ignore]
    public void remove_leading_zeros_empty()
        {
        Assert.IsNull(Texting.remove_leading_zeros(null), "remove_leading_zeros must return an empty string when input argument is an empty string value.");
        }

    private static Dictionary<string, string> the_test_value_list = new Dictionary<string, string>
        {
        {"0",       "0"},
        {"1",       "1"},
        {"01",      "1"},
        {"007",     "7"},
        {"00a0",   "a0"},
        {"00a00", "a00"},
        };

    /** @test Test a list of remove_leading_zeros cases.
    */
    [TestMethod]
    //[Ignore]
    public void remove_leading_zeros_testlist()
        {
        foreach( var test_value in the_test_value_list )
            {
            var result = Texting.remove_leading_zeros(test_value.Key);
            Assert.AreEqual(test_value.Value, result, "remove_leading_zero did not return expected result.");
            }
        }

    /** @test 
    */
    [TestMethod]
    //[Ignore]
    public void get_string()
        {
        var test_string_list = new List<string>
            {
            string.Empty
            };

        foreach( var test_string in test_string_list )
            {
            Assert.AreEqual(test_string, Texting.get_string(test_string));
            Assert.AreEqual(test_string, Texting.get_string(string.Format("{0}", test_string)));
            }
        }

    private static readonly Dictionary<char, ushort> the_unicode_data = new()
        {
            {'A', 0x0041},
            {'z', 0x007a},

            {'§', 0x00a7},
            {'°', 0x00b0},
            {'µ', 0x00b5},
            {'Ö', 0x00d6},
            {'ß', 0x00df},
            {'é', 0x00e9},
            {'€', 0x20ac},
        };

    /** @test
    */
    [TestMethod]
    //[Ignore]
    public void to_byte_array()
        {
        foreach( var test_entry in the_unicode_data )
            {
            var data  = Texting.to_byte_array(test_entry.Key.ToString());
            var value = (ushort)((data[1] << 8) | data[0]);
            Assert.AreEqual(test_entry.Value, value, "to_byte_array string '{0}' did not convert to expected value.", test_entry.Key);
            }
        }

    /** @test
    */
    [TestMethod]
    //[Ignore]
    public void from_byte_array()
        {
        foreach( var test_entry in the_unicode_data )
            {
            byte[] test_array = new byte[]
                {
                (byte)(test_entry.Value & 0xff),
                (byte)((test_entry.Value >> 8) & 0xff),
                };
            var value = Texting.from_byte_array(test_array);
            Assert.AreEqual(test_entry.Key.ToString(), value, "from_byte_array string '{0}' did not convert to expected value.", test_entry.Key);
            }
        }
    }
}
