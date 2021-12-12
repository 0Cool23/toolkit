/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2020

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_Application app.Application
    @{
    @details  Abstract base class for argument parsing.

    @ingroup  REF_libApplication
    @}
*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

using libApplication.arg;
using libApplication.io;
using libApplication.toolkit;

namespace libApplication.app
{
/** @ingroup REF_Application

    @class   Application

    @brief   Class to handle common application tasks like argument parsing,
             logging, usage and license information.
*/
public class Application
    {
    /** @details Flag which defines exception handling.
                 This property defines the handling of exceptions within the
                 application. The default value is false which means that all
                 exceptions are caught, logged and the main function returns
                 setting an ExitCode value different to 0. If set to true the
                 application class will only set an ExitCode value different
                 to 0 and then forward the exception.
    */
    public bool ForwardExceptions
        {
        get;
        set;
        } = false;

    /** @details Phrase with a short description to what the application does.
                 If this value is set it is appended to the string returned by
                 application_info.
    */
    public string Description
        {
        get;
        private set;
        } = null;

    /** @details Instance of an @ref REF_iLogger interface to write logging output.
                 If this value is set to an instance implementing the @ref REF_iLogger
                 interface. This interface is used to write logging messages to.
                 The default value is null, which means that no logging is done.
    */
    public iLogger Logger
        {
        get;
        private set;
        } = null;

    private List<ArgEntry> m_arg_table = null;

    #region default arguments
        private readonly ArgEntry m_arg_help      = new ArgEntry('h',  "help",        ArgType.NO_ARGUMENT, false, new Argument(), null, Properties.language.HELP_HELP_INFO);
        private readonly ArgEntry m_arg_license   = new ArgEntry('l',  "license",     ArgType.NO_ARGUMENT, false, new Argument(), null, Properties.language.HELP_LICENSE_INFO);
        private readonly ArgEntry m_arg_verbose   = new ArgEntry('v',  "verbose",     ArgType.NO_ARGUMENT, false, new Argument(), null, Properties.language.HELP_VERBOSITY_INFO);
        private readonly ArgEntry m_arg_build_info = new ArgEntry('\0', "build-info", ArgType.NO_ARGUMENT, false, new Argument(), null, Properties.language.HELP_BUILD_INFO);
    #endregion

    /** @details   This method attaches an @ref REF_iLogger interface and set the
                   verbose level.

        @param[in] data_logger Instance of an @ref REF_iLogger interface or null to
                   stop logging.
    */
    public void attach( iLogger data_logger )
        {
        Logger = data_logger;
        if( Logger == null )
            {
            return;
            }
        Logger.verbosity = m_arg_verbose.param.Count;
        }

    /** @details Method to override with application procedure. This method is called
                 after configuration and argument parsing.
                 
    */
    protected virtual void run()
        {
        }

    /** @details Method to override and with write an usage example.
    */
    protected virtual void display_usage_example()
        {
        }

    private readonly Assembly m_assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();

    protected virtual Version AsmVersion
        {
        get;
        set;
        } = new Version(0, 0, 0, 0);

    protected virtual Version ApiVersion
        {
        get;
        set;
        } = new Version(0, 0, 0, 0);

    private string application_info()
        {
        string result = m_assembly.GetName().Name;
        var copyright = ((AssemblyCopyrightAttribute)m_assembly.GetCustomAttribute(typeof(AssemblyCopyrightAttribute))).Copyright;

        result += string.Format(CultureInfo.CurrentCulture, " - Copyright \u00a9 {0} {1}", DateTime.Now.Year, copyright);
        result += " (version " + AsmVersion.ToString() + ")";

        if( !string.IsNullOrEmpty(Description) )
            {
            result += Environment.NewLine + "  " + Description + Environment.NewLine;
            }

        return result + Environment.NewLine;
        }

    private void configure( AppConfig app_config )
        {
        if( app_config == null )
            {
            throw new ArgumentNullException(Properties.language.APP_CONFIG_NULL_EXCEPTION);
            }

        // if configuration is valid we first want to forward exceptions and enable logging.  
        ForwardExceptions = app_config.forward_exceptions;

        // todo: check description
        Description        = app_config.description;

        attach(app_config.logger);
        }

    private bool arg_short_names_match( char table_arg_name, char entry_arg_name )
        {
        if( table_arg_name == '\0' )
            {
            return false;
            }
        return (table_arg_name == entry_arg_name);
        }

    private bool arg_long_names_match( string table_arg_name, string entry_arg_name )
        {
        if( table_arg_name == string.Empty )
            {
            return false;
            }
        return (table_arg_name == entry_arg_name);
        }

    private void add_arg_entry( ArgEntry arg_entry )
        {
        foreach( var arg in m_arg_table )
            {
            if( arg_short_names_match(arg.short_name, arg_entry.short_name) )
                {
                throw new ArgumentException(Properties.language.SHORT_ARG_EXCEPTION);
                }

            if( arg_long_names_match(arg.long_name, arg_entry.long_name) )
                {
                throw new ArgumentException(Properties.language.LONG_ARG_EXCEPTION);
                }
            }
        m_arg_table.Add(arg_entry);
        }

    private void init_arg_table( List<ArgEntry> arg_table )
        {
        m_arg_table = new List<ArgEntry>();

        // default entries required by application.
        add_arg_entry(m_arg_help);
        add_arg_entry(m_arg_verbose);
        add_arg_entry(m_arg_license);
        add_arg_entry(m_arg_build_info);

        if( arg_table == null )
            {
            return;
            }
      
        foreach( var arg_entry in arg_table )
            {
            add_arg_entry(arg_entry);
            }
        }

    private void check_arg_entries()
        {
        foreach( var arg in m_arg_table )
            {
            if( arg.is_mandatory && (arg.param.Count == 0) )
                {
                display_help();
                throw new ArgumentException(Texting.get_string(Properties.language.MANDATORY_ARG_EXCEPTION, arg.name));
                }
            }
        }

    private void display_header()
        {
        Logger.info(0, application_info());
        }

    private string get_option_entry( ArgEntry arg_entry )
        {
        string option_str;

        if( (arg_entry.short_name == 0) || (arg_entry.long_name == null) )
            {
            option_str = arg_entry.name;
            }
        else
            {
            option_str = "-" + arg_entry.short_name + " | --" + arg_entry.long_name;
            }

        return option_str;
        }

    private void display_argument( ArgEntry arg_entry )
        {
        string option_str = get_option_entry(arg_entry);
        if( arg_entry.is_mandatory )
            {
            option_str += " [mandatory]";
            }

        Logger.info(0,  "  {0}", option_str);            
        Logger.write(0, "     {0}" + Environment.NewLine, arg_entry.help_text);
        }

    private void display_argument_list()
        {
        // sort arguments
        Logger.write(0, "ARGUMENTS:");
        List<ArgEntry> sorted_list = m_arg_table.OrderBy(entry => entry.name).ToList();
        foreach( var arg_entry in sorted_list )
            {
            display_argument(arg_entry);
            }
        }

    private void display_help()
        {
        Environment.ExitCode = 2;
        if( Logger == null )
            {
            return;
            }

        display_header();

        var license = new AsmLicense();
        license.log(Logger, m_assembly, 0);

        Logger.write(0, Environment.NewLine + "Usage:");
        Logger.write(0, "  {0} [ARGUMENTS]* [-- [ARG STACK]*]" + Environment.NewLine, m_assembly.GetName().Name /* m_assembly.ManifestModule.Name*/);

        display_argument_list();

        display_usage_example();
        }

    private void check_app_help()
        {
        if( m_arg_help.param.Count <= 0 )
            {
            return;
            }

        display_help();
        throw new ArgumentException(Properties.language.USAGE_EXCEPTION);
        }

    private void display_license()
        {
        Environment.ExitCode = 2;
        if( Logger == null )
            {
            return;
            }

        display_header();
        var license = new AsmLicense();
        var verbosity = m_arg_verbose.param.Count + m_arg_license.param.Count - 1;
        license.log(Logger, m_assembly, verbosity);
        }

    private void check_app_license()
        {
        if( m_arg_license.param.Count <= 0 )
            {
            return;
            }

        display_license();
        throw new ArgumentException(Properties.language.LICENSE_EXCEPTION);
        }

    private void display_buildinfo()
        {
        Environment.ExitCode = 2;
        display_header();
        var build_info = new AsmBuildInfo();
        var verbosity = m_arg_verbose.param.Count + m_arg_build_info.param.Count - 1;
        build_info.log(Logger, m_assembly, verbosity);
        }

    private void check_app_buildinfo()
        {
        if( m_arg_build_info.param.Count <= 0 )
            {
            return;
            }

        display_buildinfo();
        throw new ArgumentException(Properties.language.BUILDINFO_EXCEPTION);
        }

    private void check_app_args()
        {
        check_app_help();
        check_app_license();
        check_app_buildinfo();
        }

    private void parse_args( string[] args, List<ArgEntry> arg_table )
        {
        ArgParser arg_parser = new ArgParser();

        init_arg_table(arg_table);
        arg_parser.process(args, m_arg_table);

        check_app_args();
        check_arg_entries();

        if( Logger != null )
            {
            Logger.verbosity = m_arg_verbose.param.Count;
            }
        }

    public int run( string[] args, List<ArgEntry> arg_table, AppConfig app_config )
        {
        try
            {
            configure(app_config);
            parse_args(args, arg_table);

            // start application.
            run();
            }
        catch( Exception except )
            {
            if( Environment.ExitCode == 0 )
                {
                Environment.ExitCode = 1;
                }

            if( ForwardExceptions )
                {
                throw;
                }

            Logger?.error(0, Texting.get_string(Properties.language.EXCEPTION, except.Message));
            }
        return Environment.ExitCode;
        }

    /** @details   Entry point for application.

        @param[in] args       Arguments for application as an array of strings.
        @param[in] arg_table  List of ArgEntry objects which are updated by parsing
                              arguments.
        @param[in] app_config Set of configuration to be used by the application.
    
        @return    Environment.ExitCode will be returned as integer value. In case of
                   an exception, the value will be a non zero result.
    */
    public static int main( string[] args, List<ArgEntry> arg_table, AppConfig app_config )
        {
        Environment.ExitCode = 0;
        
        Application application = new Application();
        return application.run(args, arg_table, app_config);
        }
    }
}
