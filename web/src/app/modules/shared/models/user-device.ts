export interface UserDevice {
  deviceId: string;
  deviceToken: string;
  dimensions: string;
  hardware: string;
  manufacturer: string;
  systemVersion: string;
  deviceType: string;
  fontScale: number;
  apiLevel: number;
  freeStorage: number;
  totalStorage: number;
  totalMemory: number;
  locationEnabled: boolean;
  isTablet: boolean;
  hasCamera: boolean;
  updatedAt: Date;
  id: string;
  createdAt: Date;
  deleted: boolean;
}
