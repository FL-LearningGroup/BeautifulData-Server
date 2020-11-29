using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.DotNetCoreKnowledage
{
    public class Resource
    {
        public IResourceProperties _properties;
        public IResourceProperties Properties { get => (this._properties = this._properties ?? new ResourceProperties()); set => this._properties = value; }
    }

    public interface IResourceProperties
    {
        public string Type { get;}
    }
    public class ResourceProperties: IResourceProperties
    {
        public string Type { get => "None"; }
    }
    public interface IBook: IResourceProperties
    {
        public int PageNumber { get; }
    }
    public interface IPencil : IResourceProperties
    {
        public int Length { get; }
    }

    public class Book: IBook
    {
        public string Type { get => "Book"; }
        public int PageNumber { get => 10; }
    }

    public class Pencil: IPencil
    {
        public string Type { get => "Pencil"; }
        public int Length { get => 2; }
    }
}
