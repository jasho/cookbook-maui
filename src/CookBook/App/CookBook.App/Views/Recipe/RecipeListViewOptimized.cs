using CookBook.App.Resources;
using CookBook.App.Resources.Fonts;
using CookBook.App.Resources.Styles;
using CookBook.App.Resources.Texts;
using CookBook.App.ViewModels;
using CookBook.Common.Models;

namespace CookBook.App.Views;

public class RecipeListViewOptimized : ContentPageBase
{
    private readonly RecipeListViewModel viewModel;

    public RecipeListViewOptimized(RecipeListViewModel viewModel)
        : base(viewModel)
    {
        this.viewModel = viewModel;

        Title = RecipeListViewTextsStatic.Page_Title;
        Style = ContentPageStyleStatic.ContentPageStyle;
        IconImageSource = new FontImageSource { FontFamily = Fonts.FontAwesome, Glyph = FontAwesomeIcons.Book };
        Content = GetRootContent();
    }

    private Grid GetRootContent()
    {
        var rootGrid = new Grid
        {
            Children =
            {
                GetContentGrid(),
                GetAddRecipeButton()
            }
        };
        rootGrid.SetValue(CompressedLayout.IsHeadlessProperty, true);

        return rootGrid;
    }

    private Grid GetContentGrid()
        => new()
        {
            HorizontalOptions = LayoutOptions.Fill,
            RowDefinitions = new RowDefinitionCollection(new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Star }),
            RowSpacing = 20,
            Margin = new Thickness(10, 0),
            Children =
            {
                GetTitleLabel(),
                GetRecipeCollectionView(),
            },
        };

    private static Label GetTitleLabel()
        => new ()
        {
            FontSize = 24,
            Style = LabelStyleStatic.BoldLabelStyle,
            Text = RecipeListViewTextsStatic.Title_Label,
        };

    private CollectionView GetRecipeCollectionView()
    {
        var recipeCollectionView = new CollectionView
        {
            VerticalOptions = LayoutOptions.Fill,
            ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Vertical) { ItemSpacing = 20 },
            ItemTemplate = GetRecipeCollectionItemTemplate()
        };
        recipeCollectionView.SetValue(Grid.RowProperty, 1);
        recipeCollectionView.SetBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(RecipeListViewModel.Items)));

        return recipeCollectionView;
    }

    private DataTemplate GetRecipeCollectionItemTemplate()
    {
        return new DataTemplate
        {
            LoadTemplate = () =>
            {
                var dataTemplateRoot = new Frame
                {
                    Padding = 0,
                    CornerRadius = 10,
                    HeightRequest = 200,
                    IsClippedToBounds = true,
                    Content = GetDataTemplateContentGrid(),
                    GestureRecognizers =
                    {
                        GetTapGestureRecognizer()
                    }
                };

                return dataTemplateRoot;
            }
        };

        TapGestureRecognizer GetTapGestureRecognizer()
        {
            var tapGestureRecognizer = new TapGestureRecognizer
            {
                Command = viewModel.GoToDetailCommand,
            };
            tapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandParameterProperty, new Binding(nameof(RecipeListModel.Id)));

            return tapGestureRecognizer;
        }

        Grid GetDataTemplateContentGrid()
            => new()
            {
                Children =
                {
                    GetImage(),
                    GetImageOverlay(),
                    GetFoodTypeFrame()
                }
            };

        Image GetImage()
        {
            var recipeItemTemplateImage = new Image
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Aspect = Aspect.AspectFill
            };
            recipeItemTemplateImage.SetBinding(Image.SourceProperty, new Binding(nameof(RecipeListModel.ImageUrl)) { TargetNullValue = "/Images/recipe_placeholder.jpg" });

            return recipeItemTemplateImage;
        }

        BoxView GetImageOverlay()
            => new()
            {
                Opacity = 0.25,
                Color = Black
            };

        Frame GetFoodTypeFrame()
        {
            var foodTypeFrame = new Frame
            {
                Margin = 22,
                Padding = new Thickness(6, 0),
                CornerRadius = 5,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                HeightRequest = 30,
                Content = GetFoodTypeFrameContent()
            };

            foodTypeFrame.SetBinding(BackgroundColorProperty, new Binding(nameof(RecipeListModel.FoodType), converter: ConvertersStatic.FoodTypeToColorConverter));

            return foodTypeFrame;
        }

        HorizontalStackLayout GetFoodTypeFrameContent()
        {
            var foodTypeFrameRoot = new HorizontalStackLayout
            {
                Padding = 0,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                Spacing = 8,
                Children =
                {
                    GetFoodTypeIconLabel(),
                    GetFoodTypeLabel()
                }
            };

            return foodTypeFrameRoot;

            Label GetFoodTypeIconLabel()
            {
                var label = new Label
                {
                    VerticalOptions = LayoutOptions.Center,
                    FontFamily = Fonts.FontAwesome,
                    TextColor = White,
                };

                label.SetBinding(Label.TextProperty, new Binding(nameof(RecipeListModel.FoodType), converter: ConvertersStatic.FoodTypeToIconConverter));

                return label;
            }

            Label GetFoodTypeLabel()
            {
                var label = new Label
                {
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = 11,
                    Style = LabelStyleStatic.BoldLabelStyle,
                    TextColor = White
                };

                label.SetBinding(Label.TextProperty, new Binding(nameof(RecipeListModel.FoodType), converter: ConvertersStatic.FoodTypeToStringConverter));

                return label;
            }
        }
    }

    private Button GetAddRecipeButton()
        => new()
        {
            Margin = new Thickness(0, 0, 14, 9),
            Padding = 0,
            Command = viewModel.GoToCreateCommand,
            CornerRadius = 60,
            FontFamily = Fonts.Regular,
            FontSize = 50,
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.End,
            Style = ButtonStyleStatic.PrimaryButtonStyle,
            Text = RecipeListViewTextsStatic.Add_Button_Text_Phone,
            WidthRequest = 65
        };
}