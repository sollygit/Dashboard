import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map, filter } from 'rxjs/operators';
import { Filter } from '../models/filter';
import { Location } from '../models/location';
import { DeliveryOrder } from '../models/delivery-order';
import { LocationService } from './location.service';
import { AppliedFiltersService } from './applied-filters.service';
import { SourceService } from './source.service';

@Injectable()
export class FilterService {

  get filters() {
    return Array<Filter>(
      {
        property: 'branch',
        displayText: 'Branch',
        type: 'Select',
        factory: (value: Location | undefined): { (order: DeliveryOrder): boolean } => {
          return (order: DeliveryOrder): boolean => {
            if (!value)
              return true;
            if (!order.location)
              return false;
            return order.location.tradingAs === value.tradingAs;
          };
        },
        options: this.locationService.branches.pipe(map(locations => locations.map(location => ({
          displayText: `${location.name}`,
          value: location
        }))))
      } as any,
      {
        property: 'branchId',
        displayText: 'Branch',
        type: 'Select',
        factory: (value: string | undefined): { (order: DeliveryOrder): boolean } => {
          return (order: DeliveryOrder): boolean => {
            if (!value || !order.branchId) {
              return true;
            }
            return order.branchId === value;
          };
        },
        options: this.locationService.locations.pipe(map(locations => locations.map(location => ({
          displayText: `${location.locationId}`,
          value: location.locationId
        }))))
      } as any,
      {
        property: 'fulfilmentType',
        displayText: 'Type',
        type: 'Select',
        factory: (value: string | undefined): { (order: DeliveryOrder): boolean } => {
          return (order: DeliveryOrder): boolean => {
            if (!value || !order.fulfilmentType)
              return true;
            return order.fulfilmentType === value;
          };
        },
        options: of([
          { displayText: 'Delivery', value: 'Delivery' },
          { displayText: 'Pickup', value: 'Pickup' }
        ])
      } as any,
      {
        property: 'sourceId',
        displayText: 'Source',
        type: 'Select',
        factory: (value: string | undefined): { (order: DeliveryOrder): boolean } => {
          return (order: DeliveryOrder): boolean => {
            if (!value || !order.sourceId)
              return true;
            return order.sourceId === value;
          };
        },
        options: this.sourceService.sources.pipe(map(sources => sources.map(source => ({
          displayText: source.code,
          value: source.code
        }))))
      } as any,
      {
        property: 'specialOrder',
        displayText: 'Special',
        type: 'Select',
        factory: (value: boolean | undefined): { (order: DeliveryOrder): boolean } => {
          return (order: DeliveryOrder): boolean => {
            if (value === undefined || order.specialOrder === undefined || order.specialOrder === null)
              return true;
            return order.specialOrder === value;
          };
        },
        options: of([
          { displayText: 'Yes', value: true },
          { displayText: 'No', value: false }
        ])
      } as any,
      {
        property: 'pickStatus',
        displayText: 'Status',
        type: 'MultiSelect',
        factory: (value: Array<string> | undefined): { (order: DeliveryOrder): boolean } => {
          return (order: DeliveryOrder): boolean => {
            if (!value || !order.pickStatus || value.length === 0)
              return true;
            return value.some(v => v === order.pickStatus);
          };
        },
        options: of([
          { displayText: 'Not Started', value: 'Not Started' },
          { displayText: 'Part Picked', value: 'Part Picked' },
          { displayText: 'Being Picked', value: 'Being Picked' },
          { displayText: 'Pick Complete', value: 'Pick Complete' },
          { displayText: 'Packed', value: 'Packed' },
          { displayText: 'Collected', value: 'Collected' },
          { displayText: 'Delivered', value: 'Delivered' }
        ]),
        inputProps: {
          width: 200,
          placeholder: 'Select a status'
        }
      } as any
    );
  }

  constructor(private locationService: LocationService, private sourceService: SourceService, private appliedFiltersService: AppliedFiltersService) {
    this.appliedFiltersService.filters$.pipe(filter(f => f.property === 'branch')).subscribe(_branchFilter => {
      let locationFilter = this.filters.find(f => f.property === 'branchId');
      if (locationFilter) {
        locationFilter.value = undefined;
        this.appliedFiltersService.set({
          property: locationFilter.property,
          selector: locationFilter.factory(undefined)
        });
      }
    });
  }

  get(properties?: Array<string>): Array<Filter> {
    if (properties)
      return this.filters.filter(f => properties.indexOf(f.property) > -1);
    return this.filters;
  }
}
