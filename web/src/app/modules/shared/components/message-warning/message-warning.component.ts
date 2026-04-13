import { Component, OnInit, Input } from "@angular/core";

@Component({
  selector: "main-message-warning",
  templateUrl: "./message-warning.component.html",
  styleUrls: ["./message-warning.component.css"],
})
export class MessageWarningComponent implements OnInit {
  @Input() show: boolean | undefined;
  @Input() text: string | undefined = "";
  @Input() title: string | undefined = "";

  constructor() {}

  ngOnInit(): void {}
}
