using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

using libApplication.toolkit;

namespace RegressionTests.libApplication.toolkit
{
[TestClass]
public class Uuid_test
    {
    private static class StaticClass
        {
        }

    private interface iInterface
        {
        string guid_string();
        }

    private abstract class BaseClass
        :   iInterface
        {
        public virtual string guid_string()
            {
            var guid_string = Uuid.create_uuid(this.GetType()).ToString();
            Debug.WriteLine(string.Format(CultureInfo.InvariantCulture, "Guid={0} [BaseClass]", guid_string));
            return guid_string;
            }
        }

    private class ClassA
        :   BaseClass
        {
        }

    private class ClassX
        :   BaseClass
        {
        public override string guid_string()
            {
            var base_guid_string = base.guid_string();
            var this_guid_string = Uuid.create_uuid(this.GetType()).ToString();
            Debug.WriteLine(string.Format(CultureInfo.InvariantCulture, "Guid={0} [ClassX]", this_guid_string));
            Assert.AreEqual(base_guid_string, this_guid_string, "Guid strings for derived calls supposed to match.");
            return this_guid_string;
            }
        }

    private class ClassXY
        :   ClassX
        {
        }

    private static readonly Dictionary<string, Type> the_guid_dictionary = new()
        {
            {"1fd9a6d0-77cd-8c1f-0ad1-0cf350c501ef", typeof(StaticClass)},
            {"64bf55e2-c5a6-5511-a0b4-c88d74c2fd6c", typeof(iInterface)},
            {"267d09af-16b5-3639-cdba-98c6bce8c60d", typeof(BaseClass)},
            {"97a1cf29-4954-14e1-9268-700933d10bfb", typeof(ClassA)},
            {"d47bbe44-f884-503e-a868-54131f162173", typeof(ClassX)},
            {"e257e607-1166-1128-afaa-118ceee18b33", typeof(ClassXY)},
        };

    [TestMethod]
    public void create_uuid()
        {
        foreach( var guid_entry in the_guid_dictionary )
            {
            var guid = Uuid.create_uuid(guid_entry.Value);
            Assert.AreEqual(guid_entry.Key, guid.ToString(), "Guid from '{0}' type did not match expected value.", guid_entry.Value.ToString());
            }
        }
    
   [TestMethod]
    public void uuid_self_matching()
        {
        var uuid_class_a1 = Uuid.create_uuid(typeof(ClassA));

        var class_a = new ClassA();
        var uuid_class_a2 = Uuid.create_uuid(class_a.GetType());
        
        Assert.AreEqual(uuid_class_a1.ToString(), uuid_class_a2.ToString(), "Guid for class types are supposed to match.");
        }

   [TestMethod]
    public void uuid_base_matching()
        {
        var class_xy = new ClassXY();
        var _ = class_xy.guid_string();
        }

    private static iInterface get_class()
        {
        var class_xy = new ClassXY();
        return class_xy;        
        }

   [TestMethod]
    public void uuid_interface_matching()
        {
        var i_class_xy = get_class();
        var i_guid_string = i_class_xy.guid_string();
        var c_guid_string = Uuid.create_uuid(i_class_xy.GetType()).ToString();
        Assert.AreEqual(i_guid_string, c_guid_string, "Interface is supposed to keep Guid of its original type.");
        }
    }
}
