# shader-hlsl-test

wpf-shader置灰测试用例

#### GrayScale
着色器代码 (GrayScaleEffect.ps) 的主要作用是将输入图像的每个像素转换为灰度值，并根据给定的 "amount" 值插值到原始颜色中，最终实现置灰效果。

原理解释：

首先，读取像素的原始颜色。每个像素颜色具有红、绿和蓝分量，表示为 color.rgb：
```
	float4 color = tex2D(input, uv);
```
接下来，计算像素的灰度值。我们使用概率加权求和方法，该方法根据人类对不同颜色的感知灵敏度为每个颜色分量分配权重：
```
	float gray = dot(color.rgb, float3(0.299, 0.587, 0.114));
```
其中，权重值分别为：红色权重 0.299，绿色权重 0.587，蓝色权重 0.114。这些值来源于对颜色的感知特性进行量化衡量。

使用Linear Interpolation (lerp)函数进行插值。根据提供的 "amount" 值 (范围：0 到 1)，分量将从其原始颜色 (rgb) 缓和过渡到等效灰度值 (gray.xxx)：
```
	color.rgb = lerp(color.rgb, gray.xxx, amount);
```

"lerp" 函数的基本插值公式：result = x + (y - x) * a，其中 ‘x’ 是初始值，‘y’ 是目标值，‘a’ 是插值摩擦系数（在我们的示例中，即为 "amount"）。

最后，返回转换后的颜色值，以使置灰效果在整个图像上应用：

如此一来，根据提供的 "amount" 参数，该着色器将为输入图像生成适当的灰度图像。
