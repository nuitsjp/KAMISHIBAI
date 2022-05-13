# Animation of navigation

By applying Storyboard, animation can be added during navigation.

```xml
<Window ...>
    <Window.Resources>
        <Storyboard x:Key="EntryStoryboard">
            <DoubleAnimation 
                Storyboard.TargetName="MainFrame"
                Storyboard.TargetProperty="(kamishibai:NavigationFrame.Opacity)" 
                From="0" To="1" Duration="0:0:0.1"/>
        </Storyboard>
    </Window.Resources>
    <Grid>
        <kamishibai:NavigationFrame x:Name="MainFrame"
                                    EntryForwardStoryboard="{StaticResource EntryStoryboard}"
                                    EntryBackwardStoryboard="{StaticResource EntryStoryboard}"/>
    </Grid>
</Window>
```

Storyboard can be set for four different timings.

|プロパティ|説明|
|--|--|
|ExitForwardStoryboard|Apply Storyboard to the source page when navigating forward.|
|EntryForwardStoryboard|Apply Storyboard to the destination page when navigating forward.|
|ExitBackwardStoryboard|Apply Storyboard to the source page when go back.|
|EntryBackwardStoryboard|Apply Storyboard to the destination page when go back.|

For example, forward navigation are executed in the following order

1. ExitForwardStoryboard
2. 画面遷移
3. EntryForwardStoryboard

Since these are processed synchronously, the entrance animation can be connected after the exit animation.

Navigation events are executed during the animation. The details are as follows

1. **ExitForwardStoryboardの開始**
2. IPausingAsyncAware#OnPausingAsync
3. IPausingAware#OnPausing
4. INavigationFramework#Pausing
5. INavigatingAsyncAware#OnNavigatingAsync
6. INavigatingAware#OnNavigating
7. INavigationFramework#Navigating
8. **ExitForwardStoryboardの終了を待機**
9. 画面遷移
10. **EntryForwardStoryboardの開始**
11. INavigatedAsyncAware#OnNavigatedAsync
12. INavigatedAware#OnNavigated
13. INavigationFramework#Navigated
14. IPausedAsyncAware#OnPausedAsync
15. IPausedAware#OnPaused
16. INavigationFramework#Paused
17. **EntryForwardStoryboardの終了を待機**

[<< Save File Dialog](10-save-file-dialog.md)