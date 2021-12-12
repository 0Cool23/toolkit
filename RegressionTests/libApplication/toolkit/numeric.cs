/** @file

    @copyright  @ref TESTING_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)
*/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

using libApplication.toolkit;

namespace RegressionTests.libApplication.toolkit
{
/** @ingroup REF_Testing REF_Numeric

    @class   Numeric_test

    @brief   Unit test for @ref REF_Numeric class.
*/
[TestClass]
//[Ignore]
public class Numeric_test
    {
    /** @test Test to make sure that unsigned to signed and back values are converted correctly. 
    */
    [TestMethod]
    //[Ignore]
    public void to_ulong_long()
        {
        var dictionary = new Dictionary<ulong, long>
            {
                {ulong.MaxValue,     -1},
                {0x8000000000000000, long.MinValue},
                {0x7fffffffffffffff, long.MaxValue},
                {                 1, 1},
            };

        foreach( var orig_value in dictionary.Keys )
            {
            long  conv_value = Numeric.to_long(orig_value);
            ulong copy_value = Numeric.to_ulong(conv_value);

            Assert.AreEqual<long>(dictionary[orig_value], conv_value, "Unsigned {0} must match signed {1} after conversion.", dictionary[orig_value], conv_value);
            Assert.AreEqual<ulong>(orig_value, copy_value, "Unsigned values must match after reconversion from signed values.");
            }
        }

    /** @test Test to make sure that signed to unsigned and back values are converted correctly.
    */
    [TestMethod]
    //[Ignore]
    public void to_long_ulong()
        {
        var dictionary = new Dictionary<long, ulong>
            {
                {-1,            ulong.MaxValue},
                {long.MinValue, 0x8000000000000000},
                {long.MaxValue, 0x7fffffffffffffff},
                { 1,            1},
            };

        foreach( var orig_value in dictionary.Keys )
            {
            ulong conv_value = Numeric.to_ulong(orig_value);
            long  copy_value = Numeric.to_long(conv_value);

            Assert.AreEqual<ulong>(dictionary[orig_value], conv_value, "Signed {0} must match unsigned {1} after conversion.", dictionary[orig_value], conv_value);
            Assert.AreEqual<long>(orig_value, copy_value, "Signed values must match after reconversion from unsigned values.");
            }
        }

    /** @test Test is_bin_digit returned correct value for given valid or invalid characters.
    */
    [TestMethod]
    //[Ignore]
    public void is_bin_digit()
        {
        foreach( var digit in "01" )
            {
            Assert.IsTrue(Numeric.is_bin_digit(digit), "Binary digit '{0}' must match true.", digit);
            }
            
        foreach( var digit in "/2\u00bc\u0f2a" ) // unicode digits (VULGAR FRACTION ONE QUARTER, TIBETAN DIGIT HALF ONE)
            {
            Assert.IsFalse(Numeric.is_bin_digit(digit), "Binary digit '{0}' must match false.", digit);
            }
        }

    /** @test Test is_oct_digit returned correct value for given valid or invalid characters.
    */
    [TestMethod]
    //[Ignore]
    public void is_oct_digit()
        {
        foreach( var digit in "01234567" )
            {
            Assert.IsTrue(Numeric.is_oct_digit(digit), "Octal digit '{0}' must match true.", digit);
            }
            
        foreach( var digit in "/8\u0f2a\u0f30" ) // unicode digits (TIBETAN DIGIT HALF ONE, TIBETAN DIGIT HALF SEVEN)
            {
            Assert.IsFalse(Numeric.is_oct_digit(digit), "Octal digit '{0}' must match false.", digit);
            }
        }

    /** @test Test is_dec_digit returned correct value for given valid or invalid characters.
    */
    [TestMethod]
    //[Ignore]
    public void is_dec_digit()
        {
        foreach( var digit in "0123456789" )
            {
            Assert.IsTrue(Numeric.is_dec_digit(digit), "Decimal digit '{0}' must match true.", digit);
            }
            
        foreach( var digit in "/:\u1372\u2164" ) // unicode digits (ETHIOPIC NUMBER TEN, ROMAN NUMERAL FIVE)
            {
            Assert.IsFalse(Numeric.is_dec_digit(digit), "Decimal digit '{0}' must match false.", digit);
            }
        }

    /** @test Test is_hex_digit returned correct value for given valid or invalid characters.
    */
    [TestMethod]
    //[Ignore]
    public void is_hex_digit()
        {
        foreach( var digit in "0123456789abcdefABCDEF" )
            {
            Assert.IsTrue(Numeric.is_hex_digit(digit), "Hexadecimal digit '{0}' must match true.", digit);
            }
            
        foreach( var digit in "/:`g@G\u1372\u2164" ) // unicode digits (ETHIOPIC NUMBER TEN, ROMAN NUMERAL FIVE)
            {
            Assert.IsFalse(Numeric.is_hex_digit(digit), "Hexadecimal digit '{0}' must match false.", digit);
            }
        }

    private static void is_within_test<T>( T min_value, T max_value ) where T : IComparable<T>
        {
        T value = min_value;
        Assert.IsTrue(value.is_within<T>(min_value, max_value));
        Assert.IsFalse(value.is_within<T>(max_value, max_value));
        value = max_value;
        Assert.IsTrue(value.is_within<T>(min_value, max_value));
        Assert.IsFalse(value.is_within<T>(min_value, min_value));
        }

    /** @test Test is_within template function for all integer types.
    */
    [TestMethod]
    //[Ignore]
    public void is_within_range()
        {
        is_within_test(byte.MinValue,   byte.MaxValue);
        is_within_test(ushort.MinValue, ushort.MaxValue);
        is_within_test(uint.MinValue,   uint.MaxValue);
        is_within_test(ulong.MinValue,  ulong.MaxValue);
        is_within_test(sbyte.MinValue,  sbyte.MaxValue);
        is_within_test(short.MinValue,  short.MaxValue);
        is_within_test(int.MinValue,    int.MaxValue);
        is_within_test(long.MinValue,   long.MaxValue);
        }
    }
}
