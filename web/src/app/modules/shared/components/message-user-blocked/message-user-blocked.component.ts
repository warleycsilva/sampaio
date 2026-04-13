import { Component, OnInit, Input } from "@angular/core";

@Component({
  selector: "main-message-user-blocked",
  templateUrl: "./message-user-blocked.component.html",
  styleUrls: ["./message-user-blocked.component.css"],
})
export class MessageUserBlockedComponent implements OnInit {
  @Input() show: boolean | undefined;
  @Input() blockedReason: string | undefined;
  @Input() title: string | undefined;

  constructor() {}

  ngOnInit(): void {}
}
