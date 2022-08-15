using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace OpenTK.WpfEditors;

internal class VersionResourceDictionary : ResourceDictionary,
                                           ISupportInitialize
{
    private int     _initializingCount;
    private string? _assemblyName;
    private string? _sourcePath;

    public VersionResourceDictionary()
    {
        _initializingCount = 0;
    }

    public VersionResourceDictionary(string assemblyName,
                                     string sourcePath)
    {
        _initializingCount = 0;

        BeginInit();
        AssemblyName = assemblyName;
        SourcePath   = sourcePath;
        EndInit();
    }

    public string? AssemblyName
    {
        get
        {
            return _assemblyName;
        }
        set
        {
            EnsureInitialization();
            _assemblyName = value;
        }
    }

    public string? SourcePath
    {
        get
        {
            return _sourcePath;
        }
        set
        {
            EnsureInitialization();
            _sourcePath = value;
        }
    }

    private void EnsureInitialization()
    {
        if(_initializingCount <= 0)
        {
            throw new InvalidOperationException("VersionResourceDictionary properties can only be set while initializing");
        }
    }

    void ISupportInitialize.BeginInit()
    {
        BeginInit();
        _initializingCount++;
    }

    void ISupportInitialize.EndInit()
    {
        _initializingCount--;

        if(_initializingCount <= 0)
        {
            if(Source != null)
            {
                throw new InvalidOperationException("Source property cannot be initialized on the VersionResourceDictionary");
            }

            if(string.IsNullOrEmpty(AssemblyName) || string.IsNullOrEmpty(SourcePath))
            {
                throw new InvalidOperationException("AssemblyName and SourcePath must be set during initialization");
            }

            Source = new Uri($"/{AssemblyName};component/{SourcePath}", UriKind.Relative);
        }

        EndInit();
    }
}
