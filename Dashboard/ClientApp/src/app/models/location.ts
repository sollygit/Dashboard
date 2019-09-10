export class Location implements ILocation {
    locationId?: number;
    name?: string | undefined;
    isDepot?: boolean;
    tradingAs?: string | undefined;

    constructor(data?: ILocation) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.locationId = data["locationId"];
            this.name = data["name"];
            this.isDepot = data["isDepot"];
            this.tradingAs = data["tradingAs"];
        }
    }

    static fromJS(data: any): Location {
        data = typeof data === 'object' ? data : {};
        let result = new Location();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["locationId"] = this.locationId;
        data["name"] = this.name;
        data["isDepot"] = this.isDepot;
        data["tradingAs"] = this.tradingAs;
        return data; 
    }
}

export interface ILocation {
    locationId?: number;
    name?: string | undefined;
    isDepot?: boolean;
    tradingAs?: string | undefined;
}
