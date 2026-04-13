import { EPropertyColor } from "../../enums/EPropertyColor"
import { EVehicleType } from "../../enums/EVehicleType"
import { SessionUser } from "../session-user"

export class CreateLesseeVehicleCommand {
    vehicleType: EVehicleType
    color: EPropertyColor
    brandId: string
    modelId: string
    registrationPlate: string
    lesseeId: string
    vehicleCRLV: File
};
  
  