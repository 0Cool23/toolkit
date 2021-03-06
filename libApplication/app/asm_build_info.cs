/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2021

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_AsmBuildInfo app.AsmBuildInfo
    @{
    @details  Class for logging build information provided by assembly.

    @ingroup  REF_libApplication
    @}
*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace libApplication.app
{
/** @ingroup REF_AsmBuildInfo

    @class   AsmBuildInfo

    @brief   Class to log build information from BuildInfo class
             generated by inline task during compilation.
*/
public class AsmBuildInfo
    :   AsmInfo
    {
    public AsmBuildInfo()
        :   base(".Generated.BuildInfo")
        {
        }

    private static readonly List<string> the_basic_field_name_list = new()
        {
            "ProjectName",
            "AsmVersion",
            "ApiVersion",
            "BuildDate",
        };

    protected override void log_basic( FieldInfo[] field_info_list )
        {
        var basic_field_map = init_field_name_map(field_info_list, the_basic_field_name_list);
        /*
        ProjectName:    xxxxxxxx
            AsmVersion: v0.0.0.156 (2021-02-06T11:02:11+01:00)
            ApiVersion: v0.0.0.0
        */
        var log_message = string.Empty;
        log_message += string.Format(CultureInfo.InvariantCulture, "ProjectName:    {0}",
                                                                            basic_field_map["ProjectName"]) + Environment.NewLine;
        log_message += string.Format(CultureInfo.InvariantCulture, "    AsmVersion: v{0} ({1})",
                                                                            basic_field_map["AsmVersion"],
                                                                            basic_field_map["BuildDate"]) + Environment.NewLine;
        log_message += string.Format(CultureInfo.InvariantCulture, "    ApiVersion: v{0}",
                                                                            basic_field_map["ApiVersion"]);
        m_logger.info(0, log_message);
        }

    private static readonly List<string> the_extended_field_name_list = new()
        {
            "BuildHost",
            "BuildType",
            "BranchName",
            "CommitHash",
            "CommitDate",
            "IsClean",
            "InSync",
        };

    private static readonly Dictionary<bool, string> the_git_status = new()
        {
            {true,  "clean"},
            {false, "dirty"},
        };

    private static string is_clean_string( bool is_clean, bool in_sync )
        {
        return the_git_status[(is_clean && in_sync)];
        }

    private static readonly Dictionary<bool, string> the_sync_status = new()
        {
            {true,  "in sync"},
            {false, "out of sync"},
        };

    private static string in_sync_string( bool in_sync)
        {
        return the_sync_status[(in_sync)];
        }

    protected override void log_extended( FieldInfo[] field_info_list )
        {
        var extended_field_map = init_field_name_map(field_info_list, the_extended_field_name_list);
        /*
            BuildHost:  xxx.xxx.xxx
            BuildHost:  Debug
            GitInfo:    develop (2021-02-03T07:26:52+01:00)
                        7f7496a0d775523c040a14de01b8696bd4a56cb4
                        clean|dirty (in sync)
        */
        var log_message = string.Empty;
        log_message += string.Format(CultureInfo.InvariantCulture, "    BuildHost:  {0}",
                                                                        extended_field_map["BuildHost"]) + Environment.NewLine;
        log_message += string.Format(CultureInfo.InvariantCulture, "    BuildType:  {0}",
                                                                        extended_field_map["BuildType"]) + Environment.NewLine;
        log_message += string.Format(CultureInfo.InvariantCulture, "    GitInfo:    {0} ({1})",
                                                                        extended_field_map["BranchName"],
                                                                        extended_field_map["CommitDate"]) + Environment.NewLine;
        log_message += string.Format(CultureInfo.InvariantCulture, "                {0}",
                                                                        extended_field_map["CommitHash"]) + Environment.NewLine;
        bool is_clean = bool.Parse(extended_field_map["IsClean"]);
        bool in_sync  = bool.Parse(extended_field_map["InSync"]);
        log_message += string.Format(CultureInfo.InvariantCulture, "                {0} ({1})",
                                                                        is_clean_string(is_clean, in_sync),
                                                                        in_sync_string(in_sync));
        m_logger.info(0, log_message);
        }
    }
}
