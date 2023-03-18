using CookBook.App.Resources.Fonts;
using CookBook.App.Resources.Styles;
using CookBook.App.Resources.Texts;
using CookBook.App.ViewModels;
using CookBook.Common.Models;
using Button = Microsoft.Maui.Controls.Button;
using ColumnDefinitionCollection = Microsoft.Maui.Controls.ColumnDefinitionCollection;
using Grid = Microsoft.Maui.Controls.Grid;
using Image = Microsoft.Maui.Controls.Image;
using RowDefinitionCollection = Microsoft.Maui.Controls.RowDefinitionCollection;

namespace CookBook.App.Views;

public class IngredientListViewOptimized : ContentPageBase
{
    private readonly IngredientListViewModel viewModel;

    public IngredientListViewOptimized(IngredientListViewModel viewModel)
        : base(viewModel)
    {
        this.viewModel = viewModel;

        Title = IngredientListViewTextsStatic.Page_Title;
        Style = ContentPageStyleStatic.ContentPageStyle;
        IconImageSource = new FontImageSource { FontFamily = Fonts.FontAwesome, Glyph = FontAwesomeIcons.Seedling };

        Content = GetRootContent();
    }

    private Grid GetRootContent()
        => new() 
        {
            HorizontalOptions = LayoutOptions.Fill,
            RowDefinitions = new RowDefinitionCollection
            {
                new () { Height = GridLength.Auto },
                new () { Height = GridLength.Star }
            },
            RowSpacing=20,
            Children =
            {
                GetTitleLabel(),
                GetIngredientCollectionView(),
                GetAddIngredientButton()
            }
        };

    private static Label GetTitleLabel()
        => new()
        {
            FontSize = 24,
            Style = LabelStyleStatic.BoldLabelStyle,
            Text = IngredientListViewTextsStatic.Title_Label
        };

    private CollectionView GetIngredientCollectionView()
    {
        var ingredientCollectionView = new CollectionView
        {
            ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Vertical) { ItemSpacing = 8 },
            ItemTemplate = GetIngredientCollectionItemTemplate()
        };
        ingredientCollectionView.SetBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(IngredientListViewModel.Items)));

        return ingredientCollectionView;
    }

    private DataTemplate GetIngredientCollectionItemTemplate()
    {
        return new DataTemplate
        {
            LoadTemplate = () =>
            {
                var dataTemplateRoot = new Grid
                {
                    BackgroundColor = Transparent,
                    ColumnDefinitions = new ColumnDefinitionCollection
                    {
                        new() { Width = 34 },
                        new() { Width = GridLength.Star },
                        new() { Width = GridLength.Auto }
                    },
                    ColumnSpacing = 20,
                    HeightRequest = 40,
                    Children =
                    {
                        GetBackgroundBoxView(),
                        GetImageWithFrame(),
                        GetNameLabel(),
                        GetEditLabel()
                    },
                    GestureRecognizers =
                    {
                        GetDataTemplateRootGestureRecognizer()
                    }
                };

                return dataTemplateRoot;
            }
        };

        TapGestureRecognizer GetDataTemplateRootGestureRecognizer()
        {
            var tapGestureRecognizer = new TapGestureRecognizer
            {
                Command = viewModel.GoToDetailCommand
            };
            tapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandParameterProperty, new Binding(nameof(IngredientListModel.Id)));

            return tapGestureRecognizer;
        }

        BoxView GetBackgroundBoxView()
        {
            var boxView = new BoxView ()
            {
                CornerRadius = 5,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Color = ThemeStatic.ListItemBackgroundColor,
            };

            boxView.SetValue(Grid.ColumnSpanProperty, 3);

            return boxView;
        }

        Frame GetImageWithFrame()
            => new()
            {
                Margin = new Thickness(4, 0, 0, 0),
                Padding = 0,
                CornerRadius = 5,
                HeightRequest = 34,
                IsClippedToBounds = true,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 34,
                Content = GetImage()
            };

        Image GetImage()
        {
            var image = new Image
            {
                Aspect = Aspect.AspectFill,
                HeightRequest = 34,
            };
            image.SetBinding(Image.SourceProperty, new Binding(nameof(IngredientListModel.ImageUrl))
            {
                TargetNullValue = "/Images/ingredient_placeholder.jpg"
            });

            return image;
        }

        Label GetNameLabel()
        {
            var label = new Label
            {
                HorizontalOptions = LayoutOptions.Start,
                Style = LabelStyleStatic.MediumLabelStyle,
                VerticalOptions = LayoutOptions.Center,
            };
            label.SetValue(Grid.ColumnProperty, 1);
            label.SetBinding(Label.TextProperty, new Binding(nameof(IngredientListModel.Name)));

            return label;
        }

        Label GetEditLabel()
        {
            var label = new Label
            {
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 0, 12, 0),
                Text = FontAwesomeIcons.Pen,
                FontSize = 20,
                TextColor = ThemeStatic.PrimaryColor,
                FontFamily = Fonts.FontAwesome,
                GestureRecognizers =
                {
                    GetEditLabelTapGestureRecognizer()
                }
            };
            label.SetValue(Grid.ColumnProperty, 2);

            return label;
        }

        TapGestureRecognizer GetEditLabelTapGestureRecognizer()
        {
            var tapGestureRecognizer = new TapGestureRecognizer
            {
                Command = viewModel.GoToEditCommand
            };
            tapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandParameterProperty, new Binding(nameof(IngredientListModel.Id)));

            return tapGestureRecognizer;
        }
    }

    private Button GetAddIngredientButton()
    {
        var button = new Button
        {
            Margin = new Thickness(0, 0, 14, 9),
            Padding = 0,
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.End,
            Text = IngredientListViewTextsStatic.Add_Button_Text,
            Command = viewModel.GoToCreateCommand,
            CornerRadius = 60,
            FontFamily = Fonts.Regular,
            FontSize = 50,
            Style = ButtonStyleStatic.PrimaryButtonStyle,
            WidthRequest = 65
        };
        button.SetValue(Grid.RowProperty, 1);

        return button;
    }
}