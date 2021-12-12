/** @file

    @copyright  @ref LIBAPPLICATION_LGPL_2_1,
                @n GNU Lesser General Public License

    @date       2021

    @author     Goetz Olbrischewski (goetz.olbrischewski@googlemail.com)

    @defgroup REF_AsmInfo app.AsmInfo
    @{
    @details  Abstract base class for extracting assembly information.

    @ingroup  REF_libApplication
    @}
*/

using System;
using System.Collections.Generic;
using System.Reflection;

using libApplication.io;

namespace libApplication.app
{
/** @ingroup REF_AsmInfo

    @class   AsmInfo

    @brief   Class to walk through assembly tree searching for build type field
             values.
*/
public abstract class AsmInfo
    {
    private   Assembly  m_entry_assembly = null;
    protected iLogger   m_logger         = null;
    protected int       m_verbosity      = 0;

    private readonly string m_build_type_name = string.Empty;

    /** @details
    */
    public AsmInfo( string build_type_name )
        {
        if( string.IsNullOrEmpty(build_type_name) )
            {
            throw new ArgumentException(Properties.language.BUILD_TYPE_NAME_EXCEPTION);
            }
        m_build_type_name = build_type_name;
        }

    private string get_field_value_safe( FieldInfo[] field_info_list, string field_name )
        {
        foreach( var field_info in field_info_list )
            {
            if( field_info.Name == field_name )
                {
                var field_value = field_info.GetValue(null).ToString();
                return field_value;
                }
            }
        return string.Empty;
        }

    /** @details
        
        @param[in] field_info_list

        @param[in] field_name

        @return
    */
    protected string get_field_value( FieldInfo[] field_info_list, string field_name )
        {
        if( field_info_list == null )
            {
            throw new ArgumentNullException(Properties.language.FIELD_INFO_LIST_NULL_EXCEPTION);
            }
        if( string.IsNullOrEmpty(field_name) )
            {
            throw new ArgumentException(Properties.language.FIELD_NAME_NULL_EXCEPTION);
            }
        return get_field_value_safe(field_info_list, field_name);
        }

    private Dictionary<string, string> init_field_name_map_safe( FieldInfo[] field_info_list, List<string> field_name_list )
        {
        var field_name_map = new Dictionary<string, string>();
        foreach( var field_name in field_name_list )
            {
            if( field_name_map.ContainsKey(field_name) )
                {
                field_name_map[field_name] = get_field_value(field_info_list, field_name);
                }
            else
                {
                field_name_map.Add(field_name, get_field_value(field_info_list, field_name));
                }
            }
        return field_name_map;
        }

    /** @details
        
        @param[in] field_info_list

        @param[in] field_name_list

        @return
     */
    protected Dictionary<string, string> init_field_name_map( FieldInfo[] field_info_list, List<string> field_name_list )
        {
        if( field_info_list == null )
            {
            throw new ArgumentNullException(Properties.language.FIELD_INFO_LIST_NULL_EXCEPTION);
            }
        if( field_name_list == null )
            {
            throw new ArgumentException(Properties.language.FIELD_NAME_LIST_NULL_EXCEPTION);
            }
        return init_field_name_map_safe(field_info_list, field_name_list);
        }

    /** @details

        @param[in] field_info_list
    */
    protected virtual void log_basic( FieldInfo[] field_info_list )
        {
        }

    /** @details

       @param[in] field_info_list
    */
    protected virtual void log_extended( FieldInfo[] field_info_list )
        {
        }

    private void log( Type build_info_type, bool with_extended_info )
        {
        if( build_info_type != null )
            {
            var field_info_list = build_info_type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            log_basic(field_info_list);
            if( with_extended_info )
                {
                log_extended(field_info_list);
                }
            m_logger.info(0, null);
            }
        }

    private void log( Assembly assembly, bool with_extended_info )
        {
        var build_info_type = assembly.GetType(assembly.GetName().Name + m_build_type_name);
        log(build_info_type, with_extended_info);
        }

    private void log()
        {
        log(m_entry_assembly, m_verbosity >= 1);
        if( m_verbosity >= 2 )
            {
            var references_assembly_name_list = m_entry_assembly.GetReferencedAssemblies();
            foreach( AssemblyName assembly_name in references_assembly_name_list )
                {
                var assembly = Assembly.Load(assembly_name);
                log(assembly, m_verbosity >= 3);
                }
            }
        }

    private void set_logger( iLogger logger )
        {
        m_logger = logger;
        }

    private void set_entry_assembly( Assembly entry_assembly )
        {
        m_entry_assembly = entry_assembly ?? throw new ArgumentNullException(Properties.language.ASSEMBLY_NULL_EXCEPTION);
        }

    private void set_verbosity( int verbosity )
        {
        if( verbosity < 0 )
            {
            verbosity = 0;
            }
        m_verbosity = verbosity;
        }

    /** @details
 
       @param[in] logger

       @param[in] entry_assembly

       @param[in] verbosity
    */
    public void log( iLogger logger, Assembly entry_assembly, int verbosity )
        {
        if( logger == null )
            {
            return;
            }

        set_logger(logger);
        set_entry_assembly(entry_assembly);
        set_verbosity(verbosity);
        log();
        }
    }
}
