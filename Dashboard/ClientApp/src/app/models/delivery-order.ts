import { Location } from './location';
import { PackageNote } from './package-note';

export class DeliveryOrder {

  constructor(
    public warnings: string[],
    public deliveryOrderId: string,
    public transCode: string,
    public branchId: string,
    public customerId: string,
    public customerPromise: string,
    public deliveryAddress: string,
    public deliveryStatus: string,
    public requestDate: Date,
    public pickupDateTime: Date,
    public deliveryDateTime: Date,
    public truckNumber: string,
    public fulfilmentType: string,
    public sourceId: string,
    public specialOrder: boolean,
    public pickStatus: string,
    public pickStatusCompleteDateTime: Date,
    public appPacked: boolean,
    public weight: number,
    public pickers: string[],
    public packageNotes: PackageNote[],
    public location?: Location
  ) { }

  static fromJson(order: any): DeliveryOrder {
    return new DeliveryOrder(
      order.Warnings || order.warnings || [],
      order.DeliveryOrderId || order.deliveryOrderId || '',
      order.TransCode || order.transCode || '',
      order.BranchId || order.branchId || '',
      order.CustomerId || order.customerId || '',
      order.CustomerPromise || order.customerPromise || '',
      order.DeliveryAddress || order.deliveryAddress || '',
      order.DeliveryStatus || order.deliveryStatus || '',
      order.RequestDate || order.requestDate || null,
      order.PickupDateTime || order.pickupDateTime || null,
      order.DeliveryDateTime || order.deliveryDateTime || null,
      order.TruckNumber || order.truckNumber || '',
      order.FulfilmentType || order.fulfilmentType || '',
      order.SourceId || order.sourceId || '',
      (<string>order.SourceId || <string>order.sourceId || '').toUpperCase() === 'SPOR',
      order.PickStatus || order.pickStatus || '',
      order.PickStatusCompleteDateTime || order.pickStatusCompleteDateTime || null,
      order.OMUAppPacked || order.appPacked || false,
      order.Weight || order.weight || 0,
      order.Pickers || order.pickers || [],
      order.PackageNotes || order.packageNotes ? (order.PackageNotes || order.packageNotes).map((x: any) => PackageNote.fromJson(x)) : [],
      order.Location || order.location ? Location.fromJS(order.Location || order.location) : undefined
    );
  }

  getHeight(lineHeight: number): number {
    var maxLines = Math.max(1, this.pickers.length);
    return lineHeight * maxLines + 10;
  }
}
