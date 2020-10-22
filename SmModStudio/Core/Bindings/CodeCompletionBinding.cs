using System;
using System.Windows.Media;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using SmModStudio.Core.Enums;
using SmModStudio.Core.Models;

namespace SmModStudio.Core.Bindings
{

    public class CodeCompletionBinding : ICompletionData
    {

        public CodeCompletionBinding(string namespaceName, CcMemberModel member)
        {
            Image = member.Type switch
            {
                ApiMemberType.Function => Constants.ImgFunction,
                ApiMemberType.Variable => Constants.ImgVariable,
                _ => null
            };
            Text = (string.IsNullOrEmpty(namespaceName) ? string.Empty : $"{namespaceName}.") + member.Name;
            Content = (string.IsNullOrEmpty(namespaceName) ? string.Empty : $"{namespaceName}.") + member.Name;
            Description = member.Description;
            Priority = 0;
        }

        public ImageSource Image { get; }
        public string Text { get;  }
        public object Content { get; }
        public object Description { get; }
        public double Priority { get; }

        public void Complete(TextArea editor, ISegment segment, EventArgs args)
        {
            editor.Document.Replace(segment, Text);
        }

    }

}