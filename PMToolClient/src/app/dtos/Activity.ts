export interface Activity {
    id: number;
    sequence: number;
    taskName: string;
    estimate: number;
    predecessors: string;
    resource: string;
    priority: number;
    start: Date;
    finish: Date;
}
