using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ttf2mesh
{
    public class Ttf2meshBinding
    {
        const string TTF2MESH_VERSION = "1.6";  /* current library version */

        const int TTF_MAX_FILE = 32;    /* font file size limit, MB */

        const int TTF_DONE = 0;  /* operation successful */
        const int TTF_ERR_NOMEM = 1;   /* not enough memory (malloc failed) */
        const int TTF_ERR_SIZE = 2;   /* file size > TTF_MAX_FILE */
        const int TTF_ERR_OPEN = 3;   /* error opening file */
        const int TTF_ERR_VER = 4;   /* unsupported file version */
        const int TTF_ERR_FMT = 5;  /* invalid file structure */
        const int TTF_ERR_NOTAB = 6;   /* no required tables in file */
        const int TTF_ERR_CSUM = 7;   /* invalid file or table checksum */
        const int TTF_ERR_UTAB = 8;  /* unsupported table format */
        const int TTF_ERR_MESHER = 9;  /* unable to create mesh */
        const int TTF_ERR_NO_OUTLINE = 10;  /* glyph has no outline */
        const int TTF_ERR_WRITING = 11;  /* error writing file */

        /* definitions for ttf_list_match function */
        const int TTF_WEIGHT_THIN = 100;
        const int TTF_WEIGHT_EXTRALIGHT = 200;
        const int TTF_WEIGHT_LIGHT = 300;
        const int TTF_WEIGHT_NORMAL = 400;
        const int TTF_WEIGHT_MEDIUM = 500;
        const int TTF_WEIGHT_DEMIBOLD = 600;
        const int TTF_WEIGHT_BOLD = 700;
        const int TTF_WEIGHT_EXTRABOLD = 800;
        const int TTF_WEIGHT_BLACK = 900;

        /* flags and values passing to some function */
        const int TTF_QUALITY_LOW = 10;   /* default quality value for some functions */
        const int TTF_QUALITY_NORMAL = 20;   /* default quality value for some functions */
        const int TTF_QUALITY_HIGH = 50;   /* default quality value for some functions */
        const int TTF_FEATURES_DFLT = 0;   /* default value of ttf_glyph2mesh features parameter */
        const int TTF_FEATURE_IGN_ERR = 1;  /* flag of ttf_glyph2mesh to ignore uncritical mesh errors */

        /* lenght of userdata array in ttf_t and ttf_glyph_t structures */
        const int TTF_GLYPH_USERDATA = 4;/* lenght of userdata array in ttf_t */
        const int TTF_FILE_USERDATA = 4;/* lenght of userdata array ttf_glyph_t */

        [StructLayout(LayoutKind.Sequential)]
        struct ttf_point
        {

        }

        [StructLayout(LayoutKind.Sequential)]
        struct ttf_outline
        { 
        
        }

        [StructLayout(LayoutKind.Sequential)]
        struct ttf_mesh
        { 

        }

        [StructLayout(LayoutKind.Sequential)]
        struct ttf_glyph
        {
            /* general fields */
            public int index;                    /* glyph index in font */
            public int symbol;                   /* utf-16 symbol */
            public int npoints;                  /* total points within all contours */
            public int ncontours;                /* number of contours in outline */
            public uint composite;               /* it is composite glyph */
            public uint reservedFlags;

            /* horizontal glyph metrics */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public float[] xbounds;               /* min/max values   along the x coordinate */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public float[] ybounds;               /* min/max values   along the y coordinate */
            public float advance;                /* advance width */
            public float lbearing;               /* left side bearing */
            public float rbearing;               /* right side bearing = aw - (lsb + xMax - xMin) */

            /* glyph outline */
            public IntPtr outline;               /* original outline of the glyph or NULL */

            /* for external use */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = TTF_GLYPH_USERDATA)]
            public IntPtr[] userdata;
        }


        /*[DllImport("ttf2mesh.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ttf_glyph2mesh();*/
    }
}
