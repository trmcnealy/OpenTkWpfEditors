using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace OpenTK.WpfEditors.Converters;

internal class ListConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context,
                                        Type                    sourceType)
    {
        return true;
    }

    public override bool CanConvertTo(ITypeDescriptorContext? context,
                                      Type                    destinationType)
    {
        return destinationType == typeof(string);
    }

    public override object? ConvertFrom(ITypeDescriptorContext?           context,
                                        System.Globalization.CultureInfo? culture,
                                        object?                           value)
    {
        if(value == null)
        {
            return null;
        }

        string?      names = value as string;
        List<object> list  = new();

        if(names == null)
        {
            list.Add(value);
        }
        else
        {
            if(string.IsNullOrEmpty(names))
            {
                return null;
            }

            foreach(string name in names.Split(','))
            {
                list.Add(name.Trim());
            }
        }

        return new ReadOnlyCollection<object>(list);
    }

    public override object? ConvertTo(ITypeDescriptorContext?           context,
                                      System.Globalization.CultureInfo? culture,
                                      object?                           value,
                                      Type                              destinationType)
    {
        if(destinationType != typeof(string))
        {
            throw new InvalidOperationException("Can only convert to string.");
        }
        
        if(value is not IList strs)
        {
            return null;
        }

        StringBuilder sb    = new();
        bool          first = true;
        foreach(object? o in strs)
        {
            if(o == null)
            {
                throw new InvalidOperationException("Property names cannot be null.");
            }

            if(o is not string s)
            {
                throw new InvalidOperationException("Does not support serialization of non-string property names.");
            }

            if(s.Contains(','))
            {
                throw new InvalidOperationException("Property names cannot contain commas.");
            }

            if(s.Trim().Length != s.Length)
            {
                throw new InvalidOperationException("Property names cannot start or end with whitespace characters.");
            }

            if(!first)
            {
                sb.Append(", ");
            }

            first = false;

            sb.Append(s);
        }

        return sb.ToString();
    }
}
