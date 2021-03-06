/** @file

    @author Auto generated source file

    @{
    @page PAGE_BuildInfo BuildInfo

    @section SEC_BuildInfo_RegressionTests BuildInfo RegressionTests

    <table border='0'>
    <tr><td colspan='3'><b>Compilation</b></td></tr>
      <tr><td>&nbsp;</td><td><b>BuildHost:</b></td><td>Pegasus.</td></tr>
      <tr><td>&nbsp;</td><td><b>BuildDate:</b></td><td>2021-12-12T18:38:47+01:00</td></tr>
      <tr><td>&nbsp;</td><td><b>BuildType:</b></td><td>Release</td></tr>
    <tr><td colspan='3'><b>Version</b></td></tr>
      <tr><td>&nbsp;</td><td><b>Assembly:</b></td><td>0.0.0.35</td></tr>
      <tr><td>&nbsp;</td><td><b>API:</b></td>     <td>0.0.0.0</td></tr>
    <tr><td colspan='3'><b>Git Repository</b></td></tr>
      <tr><td>&nbsp;</td><td><b>BranchName:</b></td><td>develop</td></tr>
      <tr><td>&nbsp;</td><td><b>CommitDate:</b></td><td>2021-12-12T17:58:36+01:00</td></tr>
      <tr><td>&nbsp;</td><td><b>CommitHash:</b></td><td>d5cfd79ca5be64df13393348791f73defa8bce27</td></tr>
      <tr><td>&nbsp;</td><td><b>Clean:</b></td>    <td>false</td></tr>
      <tr><td>&nbsp;</td><td><b>Synced:</b></td>   <td>true</td></tr>
    </table>
    @}

    @defgroup REF_BuildInfo_RegressionTests generated.BuildInfo
    @{
    @brief  Class with compile time build information properties.

    @ingroup @REF_RegressionTests

    @}

*/

using System;

namespace RegressionTests.Generated
{
/**
@ingroup REF_BuildInfo_RegressionTests

@class BuildInfo

*/
public static class BuildInfo
    {
    // Project information
    public const string    ProjectName         = "RegressionTests";
    // Compile information
    public const string    BuildHost           = "Pegasus.";
    public const string    BuildType           = "Release";
    public static readonly DateTime BuildDate  = DateTime.Parse("2021-12-12T18:38:47+01:00");
    // Git repository information
    public const string    BranchName          = "develop";
    public const string    CommitHash          = "d5cfd79ca5be64df13393348791f73defa8bce27";
    public static readonly DateTime CommitDate = DateTime.Parse("2021-12-12T17:58:36+01:00");
    public const bool      IsClean             = false;
    public const bool      InSync              = true;
    // Version information
    #pragma warning disable IDE0079 // unnecessary pragma warning
    #pragma warning disable IDE0090 // simplify new(...)
    public static readonly Version  AsmVersion = new Version(0, 0, 0, 35);
    public static readonly Version  ApiVersion = new Version(0, 0, 0, 0);
    #pragma warning restore IDE0090
    #pragma warning restore IDE0079
    }
}
