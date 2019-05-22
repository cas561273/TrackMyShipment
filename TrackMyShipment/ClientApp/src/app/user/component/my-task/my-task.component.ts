import { Component } from '@angular/core';
import { Objective } from "../../../models/Objective";
import { Carrier } from "../../../models/Carrier";
import { UserService } from "../../shared/userService";

@Component({
  selector: 'my-task',
  templateUrl: './my-task.component.html',
  styleUrls: ['./my-task.component.css']
})
export class MyTaskComponent  {

  tasks: Objective[];
  constructor(private userService: UserService) { }

  ngOnInit() {
    this.userService.getMyTask().subscribe((data) => {
      this.tasks = data.data as Objective[];
      console.log(this.tasks);
    });
  }

}
