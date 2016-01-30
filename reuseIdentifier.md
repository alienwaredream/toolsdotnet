
```
When writing your custom table view cell's code, make sure your cell is a subclass of SWTableViewCell:

#import <SWTableViewCell.h>

@interface MyCustomTableViewCell : SWTableViewCell

@property (weak, nonatomic) UILabel *customLabel;
@property (weak, nonatomic) UIImageView *customImageView;

@end
If you are using a separate nib and not a prototype cell, you'll need to be sure to register the nib in your table view:

- (void)viewDidLoad
{
    [super viewDidLoad];

    [self.tableView registerNib:[UINib nibWithNibName:@"MyCustomTableViewCellNibFileName" bundle:nil] forCellReuseIdentifier:@"MyCustomCell"];
}
Then, in the tableView:cellForRowAtIndexPath: method of your UITableViewDelegate (usually your view controller), initialize your custom cell:

- (UITableViewCell*)tableView:(UITableView*)tableView cellForRowAtIndexPath:(NSIndexPath*)indexPath
{
    static NSString *cellIdentifier = @"MyCustomCell";

    MyCustomTableViewCell *cell = (MyCustomTableViewCell *)[tableView dequeueReusableCellWithIdentifier:cellIdentifier 
                                                                                           forIndexPath:indexPath];

    cell.leftUtilityButtons = [self leftButtons];
    cell.rightUtilityButtons = [self rightButtons];
    cell.delegate = self;

    cell.customLabel.text = @"Some Text";
    cell.customImageView.image = [UIImage imageNamed:@"MyAwesomeTableCellImage"];
    [cell setCellHeight:cell.frame.size.height];
    return cell;
}
```

source: https://github.com/CEWendel/SWTableViewCell