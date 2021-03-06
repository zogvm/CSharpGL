﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;

namespace CSharpGL
{
    /// <summary>
    /// 用在GLSwitch类型的属性上。
    /// </summary>
    public class GLSwithEditor : UITypeEditor
    {

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            // 指定为模式窗体属性编辑器类型 
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            // 打开属性编辑器修改数据 
            var frmGLSwitchListEditor = new FormGLSwitchEditor(value as GLSwitch);
            if (frmGLSwitchListEditor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
            }

            return value;
        }
    }
}
