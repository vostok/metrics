## 0.2.6 (06.01.2020):

- Fixed a race condition occurring in initialization of scrapable metric groups (gauges, counters, etc).

## 0.2.5 (06.01.2020):

- IntegerGauge and FloatingGauge: added `TryIncreaseTo` and `TryReduceTo` methods to track max/min values.

## 0.2.4 (16-10-2019):

- Removed sending empty summary.

## 0.2.3 (15-10-2019):

- Removed sending empty histogram buckets.
- Added `SendZeroValues` setting to `CounterConfig`.

## 0.2.2 (09-10-2019):

- Made `IMetricContextWrapper` public.

## 0.2.1 (13-09-2019):

- Fixed `CreateMultiFuncGauge` not `IDisposable` anymore.

## 0.2.0 (04-09-2019):

- Public interface primitives (such as `ICounter` and `ITimer` and `IMetricGroup`) no longer inherit from `IDisposable`.
- Following primitive factory methods now use a shared static cache to deduplicate instances of scraped primitives:
	- `CreateCounter`
	- `CreateSummary`
	- `CreateHistogram`
	- `CreateIntegerGauge`
	- `CreateFloatingGauge`
- Added a new lightweight model — `MetricDataPoint`. It's a simple alternative to `MetricEvent` that can be constructed by users.
- Added a new primitive — `MultiFuncGauge`. It allows to provide multiple metric values at once using `MetricDataPoints`.
- Added a new extension for `IMetricContext` that allows to send `MetricDataPoints` as if they were `MetricEvents`.
- All custom user-implemented scrapable primitives now use a separate internal scheduler.

## 0.1.2 (26-08-2019):

- Removed static `MetricContextProvider`.
- Now using `InvariantCulture` comparison for `MetricTag` properties.

## 0.1.1 (06-08-2019):

Fixed resizing of tags containers when appending a lot of tags at once.

## 0.1.0 (15-07-2019): 

Initial prerelease.