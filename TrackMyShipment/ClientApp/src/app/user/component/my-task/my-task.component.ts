import { Component } from '@angular/core';
import { Objective } from "../../../models/Objective";
import { Carrier } from "../../../models/Carrier";
import { UserService } from "../../shared/userService";
import { DataSharingService } from "../../../services/dataSharing";
import { Person } from "../../../models/Person";

@Component({
  selector: 'my-task',
  templateUrl: './my-task.component.html',
  styleUrls: ['./my-task.component.css']
})
export class MyTaskComponent  {

  tasks: Objective[];
  currentUser: Person;

  constructor(private userService: UserService,private dataSharingService:DataSharingService) { }

  ngOnInit() {
    this.userService.getMyTask().subscribe((data) => {
      this.tasks = data.data as Objective[];
      console.log(this.tasks);
    });

    this.dataSharingService.currentUser.subscribe(user => {
      this.currentUser = user;
    });
  }

  takeTask(task:Objective,index:number) {
    this.userService.takeTask(task).subscribe((data) => {
      if (data.state === 1) {
        this.tasks[index].status = !this.tasks[index].status;
        console.log(this.tasks[index]);
        console.log('yes');
      }
    });
  }

  changeStatus(taskId: number,index:number) {
    console.log(this.tasks[index]);
    this.userService.changeStatusTask(taskId).subscribe((data) => {
      if (data.state === 1) {
        this.tasks[index].status = !this.tasks[index].status;
      }
    });
  }

}
