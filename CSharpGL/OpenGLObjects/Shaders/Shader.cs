﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpGL
{
    /// <summary>
    /// This is the base class for all shaders (vertex and fragment). It offers functionality
    /// which is core to all shaders, such as file loading and binding.
    /// </summary>
    public class Shader
    {
        public void Create(uint shaderType, string source)
        {
            //  Create the OpenGL shader object.
            ShaderObject = GL.GetDelegateFor<GL.glCreateShader>()(shaderType);

            //  Set the shader source.
            GL.GetDelegateFor<GL.glShaderSource>()(ShaderObject, 1, new[] { source }, new[] { source.Length });
            //  Compile the shader object.
            GL.GetDelegateFor<GL.glCompileShader>()(ShaderObject);

            //  Now that we've compiled the shader, check it's compilation status. If it's not compiled properly, we're
            //  going to throw an exception.
            if (GetCompileStatus() == false)
            {
                string log = GetInfoLog();
                throw new ShaderCompilationException(
                    string.Format("Failed to compile shader with ID {0}.{1}{2}", ShaderObject, Environment.NewLine, log), log);
            }
        }

        public void Delete()
        {
            GL.GetDelegateFor<GL.glDeleteShader>()(ShaderObject);
            ShaderObject = 0;
        }

        public bool GetCompileStatus()
        {
            int[] parameters = new int[] { 0 };
            GL.GetDelegateFor<GL.glGetShaderiv>()(ShaderObject, GL.GL_COMPILE_STATUS, parameters);
            return parameters[0] == GL.GL_TRUE;
        }

        public string GetInfoLog()
        {
            //  Get the info log length.
            int[] infoLength = new int[] { 0 };
            GL.GetDelegateFor<GL.glGetShaderiv>()(ShaderObject, GL.GL_INFO_LOG_LENGTH, infoLength);
            int bufSize = infoLength[0];

            //  Get the compile info.
            StringBuilder il = new StringBuilder(bufSize);
            GL.GetDelegateFor<GL.glGetShaderInfoLog>()(ShaderObject, bufSize, IntPtr.Zero, il);
            string log = il.ToString();
            return log;
        }

        /// <summary>
        /// Gets the shader object.
        /// </summary>
        public uint ShaderObject { get; private set; }
    }
}