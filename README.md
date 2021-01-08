# Xamarin.Forms Generic Picker
A reusable and customizable generic picker for Xamarin.Forms

The purpose of this work is to have a completely customizable and reusable replacement for standard Xamarin.forms picker that allows:

- Databinding
- Scroll to download more data elements
- Search items 
- and finally pick an item.

To use it, you have to create 2 extra elements for each type of object you want to look up to using this control :

**Data Adapter**

A Data adapter class must implement IPickerDataAdapter interface, and it's usage is intended to make a call your client sevice and retrieve some data. Here's a pretty rough example calling a mocked data routine:

```c#

public class ArepaDataAdapter : IPickerDataAdapter
    {
        public async Task<DataResponse<IEnumerable>> LoadDataAsync(int offset, string searchText)
        {
            //TODO: Implement your own API client here

            var dataAccess = new ArepaDataAccess();
            var response = await dataAccess.GetList(new PickerRequest()
            {
                Search = searchText,
                Offset = offset,
                ChunkSize = 10
            });
            return response;
        }
    }
    
```

**Item Template**

An item Data template content view where you can completely customize results list items appeareance. This data template must implement a TapGestureRecognizer on any of its elements (preferable a top level layout) to call SelectedItemCommand on picker's control.

```xaml

<Grid>
  <Grid.GestureRecognizers>
    <TapGestureRecognizer 
      Command="{Binding SelectedItemCommand, Source={x:RelativeSource AncestorType={x:Type controls:GenericPicker}}}"
      CommandParameter="{Binding .}"/>
   </Grid.GestureRecognizers> 
  ...
  <!--Place your list's items layout definition here -->
 </Grid>

```


**How to trigger picker UI** 

Finally, the code to load picker page is this:

```c#
      var picker = new GenericPicker(new ArepaDataAdapter(), new DataTemplate( typeof(ArepaTemplateView)),
           (selectedItem) =>
           {
               // cast your object back to your model type and use it as you want
               var arepa = selectedItem as ArepaItem;
               
               
               // ensure to pop picker's modal view at the end of the action
               Navigation.PopModalAsync(true);
            });
       Navigation.PushModalAsync(picker);
       
```

**Here's how it works:**
<p align="center">
    <img src="https://github.com/jpmirfcb/GenericPicker/raw/master/picker.gif" width="300">
</p>
