# Composition Kata

> Recreating from [Scott's Composition Kata for CSharp](https://github.com/OdeToCode/Katas/tree/master/Composition/CS/Composition) using the .NET Core.

## Description

One day a developer was writing some logic to perform calculations on
a collection of Measurement objects with X and Y properties. The developer knew
the business would need different types of aggregations performed on the collection
(sometimes a simple sum, sometimes an average), and the business would also want
to filter measures (sometimes removing low values, sometimes removing high values).

The developer wanted to make sure all algorithms for calculating a result
would filter a collection of measurements before aggregation. After consulting
with a book on design patterns, the developer decided the Template Method pattern would be
ideal for enforcing a certain ordering on operations while still allowing subclasses to override
and change the actual filtering and the calculations.

### Base class

```csharp
    public abstract class PointsAggregator
    {
    	protected PointsAggregator(IEnumerable<Measurement> measurements)
    	{
    		Measurements = measurements;
    	}

    	public virtual Measurement Aggregate()
    	{
    		var measurements = Measurements;
    		measurements = FilterMeasurements(measurements);
    		return AggregateMeasurements(measurements);
    	}

    	protected abstract IEnumerable<Measurement> FilterMeasurements(IEnumerable<Measurement> measurements);
    	protected abstract Measurement AggregateMeasurements(IEnumerable<Measurement> measurements);

    	protected readonly IEnumerable<Measurement> Measurements;
    }
```

From this base class the developer started creating different classes to represent
different calculations (along with unit tests).

### Your tasks

Your job is to create a new calculation. A calculation to filter out measurements with low values
and then sum the X and Y values of the remaining measurements. You'll find details in the comments
of the code, and the unit tests.

You'll implement the calculation twice. Once by building on the
Template Method approach (in the Inheritance folder), and once with a compositional
approach (in the Composition folder).

## How To Start

1. Open the solution file.

2. Run the tests to make sure everything is in working order.

3. Start in the AggregationTests.cs file from the `Algorithm.Tests.Inheritance` folder.
   Uncomment the last test in the file, and make it pass by building a
   HighPassSummingAggregator (there is already a placeholder for you in the Inheritance
   folder of the Algorithm project, you just
   have to figure out what class to inherit from and how to implement the logic).

4. Do the same in the `Algorithm.Tests.Composition` folder, using classes in
   the Composition folder of the Algorithm project.

When you are finished and made all of the tests pass (9 total, you'll start with 6), take
some time to reflect on the different approaches.

## Find answers yourself

-  ## Which are the drawbacks and benefits to each?

### Inheritance
<b>Drawback</b>: The hierarchy of inherited classes can get quite convoluted- it is already 3 levels deep - for example. HighPassSummingAggregator inherits SummingAggregator which inherits PointsAggregator.

<b>Benefit</b>: Can prevent some code duplication, and I didn't have to implement all inherited methods, just the ones that I needed to override.

### Composition
<b>Drawback</b>: I had to write an extra class (HighPassSummingAggregator) to hide the use of filters and strategies. Without this class the user also has to research the filters and strategies to find out which they want to use, while for inheritance it is in the name of the class.

<b>Benefit</b>: It is much more flexible and easier to change over time.

-   ## Which one would you rather build on in the future?

<b>Composition</b>. For example, if I want to use a HighPassFilter and AveragingStrategy, it is as simple as giving those two parameters to the PointsAggregator. It is also simple to create new Filters and Strategies by implementing the corresponding interfaces with new classes.

If you were using Inheritance, to make a HighPass AveragingAggregator you would have to create a new HighPassAveragingAggregator which would likely inherit AveragingAggregator which inherits PointsAggregator. And this is not good because it just repeats the HighPass logic that is used.

-  ## Which one achieves better "reusability"?

<b>Composition</b>. As described above, to reuse the same filter on a lot of different strategies it is as simple as using the one filter class in the PointAggregator.

Whereas with inheritance, you need to make a new class every time, which repeats the same logic for the same filter. Of course you could look at changing the inheritance, or introducing the filter to be inherited somewhere, but this will get more complicated over time.

