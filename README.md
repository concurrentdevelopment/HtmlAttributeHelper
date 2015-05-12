# HtmlAttributeHelper

A useful extension method to help output model metadata and selected view data to your html elements.

## Usage

If you have a model like so:

    public class ViewModel
    {
        [AdditionalMetadata("title", "This is the title")]
        [AdditionalMetadata("data-autocomplete-url", "/Api/Autocomplete/Name")]
        public string Name { get; set; }
    
        [AdditionalMetadata("class", "big-text")]
        public string Description { get; set; }
    
        [AdditionalMetadata("class", "grey")]
        public string AnotherField { get; set; }
    }
    
Then if you create an editor template called String.cshtml in ./Views/Shared/EditorTemplates like so:

    @model String

    @Html.TextBox("", ViewData.GetHtmlAttributes())
    
Then putting this in a view

    @model ViewModel

    @Html.EditorFor(m => m.Name)
    
    @Html.EditorFor(m => m.Description)
    
    @Html.EditorFor(m => m.AnotherField, new { @class = "black" })

will render something like this:

    <input type="text" id="Name" name="Name" title="This is the title" data-autocomplete-url="/Api/Autocomplete/Name" />
    <input type="text" id="Description" name="Description" class="big-text" />
    <input type="text" id="AnotherField" name="AnotherField" class="black grey" />
