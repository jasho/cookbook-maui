using CookBook.App.Resources;
using CookBook.App.Resources.Fonts;
using CookBook.App.Resources.Styles;
using CookBook.App.Resources.Texts;
using CookBook.App.ViewModels;
using CookBook.Common.Models;

namespace CookBook.App.Views;

public class RecipeListViewOptimized : ContentPageBase
{
    private RecipeListViewModel viewModel;

    public RecipeListViewOptimized(RecipeListViewModel viewModel)
        : base(viewModel)
    {
        this.viewModel = viewModel;

        Title = RecipeListViewTexts.Page_Title;
        Style = ContentPageStyleStatic.ContentPageStyle;
        IconImageSource = new FontImageSource { FontFamily = Fonts.FontAwesome, Glyph = FontAwesomeIcons.Book };
        Content = GetRootContent();
    }

    private Grid GetRootContent()
    {
        var rootGrid = new Grid();
        rootGrid.SetValue(CompressedLayout.IsHeadlessProperty, true);

        var contentGrid = new Grid
        {
            HorizontalOptions = LayoutOptions.Fill,
            RowDefinitions = new RowDefinitionCollection(new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Star }),
            RowSpacing = 20,
            Margin = new Thickness(10, 0)
        };

        var titleLabel = GetTitleLabel();
        contentGrid.Children.Add(titleLabel);
            
        var recipeCollectionView = GetRecipeCollectionView();
        contentGrid.Children.Add(recipeCollectionView);

        rootGrid.Children.Add(contentGrid);
            
        var addRecipeButton = GetAddRecipeButton();
        rootGrid.Children.Add(addRecipeButton);

        return rootGrid;
    }

    private static Label GetTitleLabel()
    {
        var titleLabel = new Label
        {
            FontSize = 24,
            Style = LabelStyleStatic.BoldLabelStyle,
            Text = RecipeListViewTexts.Title_Label,
        };
        titleLabel.SetValue(Grid.RowProperty, 0);
        return titleLabel;
    }

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
                    Content = GetContentGrid()
                };
                var tapGestureRecognizer = new TapGestureRecognizer
                {
                    Command = viewModel.GoToDetailCommand,
                };
                tapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandParameterProperty, new Binding(nameof(RecipeListModel.Id)));
                dataTemplateRoot.GestureRecognizers.Add(tapGestureRecognizer);

                return dataTemplateRoot;
            }
        };

        Grid GetContentGrid()
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
            recipeItemTemplateImage.SetValue(Grid.RowProperty, 0);
            recipeItemTemplateImage.SetBinding(Image.SourceProperty, new Binding(nameof(RecipeListModel.ImageUrl)) { TargetNullValue = "/Images/recipe_placeholder.jpg" });

            return recipeItemTemplateImage;
        }

        BoxView GetImageOverlay()
        {
            var imageOverlay = new BoxView
            {
                Opacity = 0.25,
                Color = Black
            };
            imageOverlay.SetValue(Grid.RowProperty, 0);

            return imageOverlay;
        }

        Frame GetFoodTypeFrame()
        {
            var foodTypeFrame = new Frame
            {
                Margin = 22,
                Padding = new Thickness(6, 0),
                CornerRadius = 5,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                HeightRequest = 30
            };

            foodTypeFrame.SetValue(Grid.RowProperty, 0);
            foodTypeFrame.SetBinding(Microsoft.Maui.Controls.Frame.BackgroundColorProperty, new Binding(nameof(RecipeListModel.FoodType), converter: ConvertersStatic.FoodTypeToColorConverter));

            foodTypeFrame.Content = GetFoodTypeFrameContent();

            return foodTypeFrame;
        }

        HorizontalStackLayout GetFoodTypeFrameContent()
        {
            var foodTypeFrameRoot = new HorizontalStackLayout
            {
                Padding = 0,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                Spacing = 8
            };

            var foodTypeIconLabel = GetFoodTypeIconLabel();
            foodTypeFrameRoot.Children.Add(foodTypeIconLabel);

            var foodTypeLabel = GetFoodTypeLabel();
            foodTypeFrameRoot.Children.Add(foodTypeLabel);

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
            Text = IngredientListViewTexts.Add_Button_Text_Phone,
            WidthRequest = 65
        };

    protected override void OnAppearing() {
        base.OnAppearing();
        TimingHelper.Log("END");
    }
}